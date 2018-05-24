using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization;
using System.Web.UI;

namespace ProperControls.Pages.Persistence
{
    /// <summary>
    /// Stores DataContact entities using DataContract Serializer.
    /// </summary>
    public class DataContractStateBag
    {
        private Page page;
        private Dictionary<string, object> EntityStateItems;
        private HashSet<Type> EntityStateTypes { get; set; }
        private string EntityStateString { get; set; }
        private HiddenField entityState;
        public DataContractStateBag(Page page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            this.page = page;
            this.page.PreLoad += page_PreLoad;
            this.page.PreRenderComplete += page_PreRenderComplete;
            this.page.Init += page_Init;
        }

        /// <summary>
        /// Registers the entity state field for persistance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_Init(object sender, EventArgs e)
        {
            this.entityState = new HiddenField();

            ScriptManager sm = ScriptManager.GetCurrent(this.page);
            if (sm == null || !sm.SupportsPartialRendering)
            {
                this.page.Form.Controls.Add(this.entityState);
            }
            else
            {
                UpdatePanel panel = new UpdatePanel();
                panel.ID = "updEntityState";
                panel.UpdateMode = UpdatePanelUpdateMode.Always;

                panel.ContentTemplateContainer.Controls.Add(this.entityState);
                this.page.Form.Controls.Add(panel);
            }
        }

        /// <summary>
        /// The prerender complete event occures before the page's save state.
        /// This looks like the correct place to use it, although it isn't mandatory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PreRenderComplete(object sender, EventArgs e)
        {
            SaveEntityState();

            // due to the naming container, we have to use unorthodox methods to store and retrieve the __ENTITYSTATE
            //string hidden = string.Format("<input type='hidden' name='__ENTITYSTATE' value='{0}' />", EntityStateString);
            //this.page.Form.Controls.Add(new LiteralControl(hidden));
            this.entityState.Value = EntityStateString;
        }

        /// <summary>
        /// The prerender complete event occures after the view state was loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PreLoad(object sender, EventArgs e)
        {
            //EntityStateString = page.Request["__ENTITYSTATE"];
            EntityStateString = this.entityState.Value;

            LoadEntityState();
        }

        private void LoadEntityState()
        {
            // check for the state
            string state = EntityStateString;
            if (string.IsNullOrEmpty(state))
                return;

            // convert to binary
            byte[] data = Convert.FromBase64String(state);

            // use memory stream
            using (MemoryStream ms = new MemoryStream(data))
            {
                // decompress
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true))
                {
                    // deserialize types
                    BinaryFormatter formatter = new BinaryFormatter();
                    this.EntityStateTypes = (HashSet<Type>)formatter.Deserialize(zip);

                    // deserialize entities
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), EntityStateTypes);
                    this.EntityStateItems = (Dictionary<string, object>)serializer.ReadObject(zip);
                }
            }
        }

        public void SaveEntityState()
        {
            if (this.EntityStateItems == null || this.EntityStateItems.Count == 0)
                return;

            // prepare memory
            using (MemoryStream ms = new MemoryStream())
            {
                // compress
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    // prepare types
                    foreach (object item in this.EntityStateItems.Values) { EntityStateTypes.Add(item.GetType()); }

                    // serialize types
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(zip, this.EntityStateTypes);

                    // serialize entities
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), EntityStateTypes);
                    serializer.WriteObject(zip, this.EntityStateItems);
                }

                // convert compressed byte array to string
                byte[] buffer = ms.ToArray();

                // render the state
                EntityStateString = Convert.ToBase64String(buffer);
            }
        }

        private void EnsureStateBag()
        {
            if (this.EntityStateItems == null)
            {
                this.EntityStateItems = new Dictionary<string, object>();
                this.EntityStateTypes = new HashSet<Type>();
            }
        }
        public object this[string key]
        {
            get
            {
                EnsureStateBag();
                object item;
                this.EntityStateItems.TryGetValue(key, out item);
                return item;
            }
            set
            {
                if (value != null)
                {
                    Type t = value.GetType();

                    bool iteratorHasItems = false;
                    IEnumerable iterator = value as IEnumerable;
                    if (iterator != null)
                    {
                        // get the type of the first item
                        foreach (var item in iterator)
                        {
                            iteratorHasItems = true;
                            t = item.GetType();
                            break;
                        }
                    }

                    if (iterator == null || (iterator != null && iteratorHasItems))
                    {
                        if (!Attribute.IsDefined(t, typeof(DataContractAttribute)))
                            throw new InvalidOperationException("Class must have a DataContract attribute");
                    }
                }

                EnsureStateBag();
                this.EntityStateItems[key] = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace ProperControls.Pages.Compression
{
    public class GZipPersister : PageStatePersister
    {
        public GZipPersister(Page page) : base(page) { }

        private string State
        {
            get
            {
                Type t = typeof(Page);
                PropertyInfo p = t.GetProperty("RequestViewStateString", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                return (string)p.GetValue(this.Page, null);
            }
            set
            {
                Type t = typeof(Page);
                PropertyInfo p = t.GetProperty("ClientState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                p.SetValue(this.Page, value, null);
            }
        }

        /// <summary>
        /// Load the state.
        /// </summary>
        public override void Load()
        {
            // check for the state
            string state = State;
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
                    // deserialize
                    LosFormatter formatter = new LosFormatter();
                    Pair pair = (Pair)formatter.Deserialize(zip);

                    // extract objects
                    base.ViewState = pair.First;
                    base.ControlState = pair.Second;
                }
            }
        }

        /// <summary>
        /// Save the state.
        /// </summary>
        public override void Save()
        {
            // prepare memory
            using (MemoryStream ms = new MemoryStream())
            {
                // compress
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    Pair pair = new Pair(base.ViewState, base.ControlState);
                    LosFormatter formatter = new LosFormatter();

                    // serialize
                    formatter.Serialize(zip, pair);
                }

                // convert compressed byte array to string
                byte[] buffer = ms.ToArray();

                // render the state
                State = Convert.ToBase64String(buffer);
            }
        }
    }
}

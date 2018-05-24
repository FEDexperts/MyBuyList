using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using ICSharpCode.SharpZipLib.Zip;
using System.Reflection;
using System.IO;

namespace ProperControls.Pages.Compression
{
    public class SharpZipPersister : PageStatePersister
    {
        private int compressionLevel;
        private const string FieldKey = "_VS";
        public SharpZipPersister(Page page) : this(page, 9) { }
        public SharpZipPersister(Page page, int compressionLevel)
            : base(page)
        {
            this.compressionLevel = compressionLevel;
        }

        private string GetRequestViewStateString()
        {
            Type t = typeof(Page);
            PropertyInfo p = t.GetProperty("RequestViewStateString", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return (string)p.GetValue(this.Page, null);
        }

        private void SetClientState(string value)
        {
            Type t = typeof(Page);
            PropertyInfo p = t.GetProperty("ClientState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            p.SetValue(this.Page, value, null);
        }

        /// <summary>
        /// Load the state.
        /// </summary>
        public override void Load()
        {
            // check for the state
            string state = GetRequestViewStateString();
            if (string.IsNullOrEmpty(state))
                return;

            // convert to binary
            byte[] data = Convert.FromBase64String(state);

            // use memory stream
            using (MemoryStream ms = new MemoryStream(data))
            {
                // decompress
                using (ZipInputStream zip = new ZipInputStream(ms))
                {
                    ZipEntry e = zip.GetNextEntry();
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
                using (ZipOutputStream zip = new ZipOutputStream(ms))
                {
                    Pair pair = new Pair(base.ViewState, base.ControlState);
                    LosFormatter formatter = new LosFormatter();

                    // serialize
                    zip.PutNextEntry(new ZipEntry(""));
                    zip.SetLevel(this.compressionLevel);
                    formatter.Serialize(zip, pair);
                }

                // convert compressed byte array to string
                byte[] buffer = ms.ToArray();

                // render the state
                SetClientState(Convert.ToBase64String(buffer));
            }
        }
    }
}

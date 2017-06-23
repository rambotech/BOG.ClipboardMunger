using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger
{
    public class ClipboardMungerScriptContainer
    {
        private string _Title;
        private string _Description;
        private string _Script;
        private int _Language;
        private string _TestData;
        private List<string> _References = new List<string>();
        private List<string> _OtherReferences = new List<string>();

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Script
        {
            get { return _Script; }
            set { _Script = value; }
        }

        public int Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        public string TestData
        {
            get { return _TestData; }
            set { _TestData = value; }
        }

        public List<string> References
        {
            get { return _References; }
            set { _References = value; }
        }

        public List<string> OtherReferences
        {
            get { return _OtherReferences; }
            set { _OtherReferences = value; }
        }
    }
}

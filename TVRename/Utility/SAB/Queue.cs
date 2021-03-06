// 
// Main website for TVRename is http://tvrename.com
// 
// Source code available at https://github.com/TV-Rename/tvrename
// 
// This code is released under GPLv3 https://github.com/TV-Rename/tvrename/blob/master/LICENSE.md
// 

// 
// The initial version of this source code was auto-generated by xsd, Version=2.0.50727.1432.
// 
// Save http://localhost:8080/sabnzbd/api?mode=queue&apikey=...&start=0&limit=9999&output=xml as sab.xml
// xsd.exe sab.xml
// xsd.exe sab.xsd /c /edb

using System.Xml.Serialization;
using System.IO;

namespace TVRename.SAB
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Queue : object, System.ComponentModel.INotifyPropertyChanged
    {
        public static Queue Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            XmlSerializer serializer = new XmlSerializer(typeof(Queue));
            try
            {
                Queue r = (Queue) serializer.Deserialize(ms);
                return r;
            }
            catch
            {
                return null;
            }
        }

        private string uniconfigField;
        private string cache_sizeField;
        private string active_langField;
        private string pausedField;
        private string sessionField;
        private string restart_reqField;
        private string power_optionsField;
        private string speedField;
        private string helpuriField;
        private string sizeField;
        private string uptimeField;
        private string refresh_rateField;
        private string my_homeField;
        private string limitField;
        private string have_quotaField;
        private string isverboseField;
        private string startField;
        private string finishField;
        private string versionField;
        private string new_rel_urlField;
        private string my_lcldataField;
        private string color_schemeField;
        private string diskspacetotal1Field;
        private string ntField;
        private string statusField;
        private string last_warningField;
        private string have_warningsField;
        private string cache_artField;
        private string sizeleftField;
        private string finishactionField;
        private string paused_allField;
        private string quotaField;
        private string newzbin_urlField;
        private string new_releaseField;
        private string pause_intField;
        private string mbleftField;
        private string diskspace1Field;
        private string scriptsField;
        private string darwinField;
        private string timeleftField;
        private string mbField;
        private string noofslotsField;
        private string etaField;
        private string diskspacetotal2Field;
        private string nzb_quotaField;
        private string loadavgField;
        private string cache_maxField;
        private string kbpersecField;
        private string speedlimitField;
        private string webdirField;
        private string queue_detailsField;
        private string left_quotaField;
        private string diskspace2Field;

        private QueueSlotsSlot[] slotsField;
        private QueueCategories[] categoriesField;

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uniconfig
        {
            get => uniconfigField;
            set
            {
                uniconfigField = value;
                RaisePropertyChanged("uniconfig");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cache_size
        {
            get => cache_sizeField;
            set
            {
                cache_sizeField = value;
                RaisePropertyChanged("cache_size");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string active_lang
        {
            get => active_langField;
            set
            {
                active_langField = value;
                RaisePropertyChanged("active_lang");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string paused
        {
            get => pausedField;
            set
            {
                pausedField = value;
                RaisePropertyChanged("paused");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string session
        {
            get => sessionField;
            set
            {
                sessionField = value;
                RaisePropertyChanged("session");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string restart_req
        {
            get => restart_reqField;
            set
            {
                restart_reqField = value;
                RaisePropertyChanged("restart_req");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string power_options
        {
            get => power_optionsField;
            set
            {
                power_optionsField = value;
                RaisePropertyChanged("power_options");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string speed
        {
            get => speedField;
            set
            {
                speedField = value;
                RaisePropertyChanged("speed");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string helpuri
        {
            get => helpuriField;
            set
            {
                helpuriField = value;
                RaisePropertyChanged("helpuri");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string size
        {
            get => sizeField;
            set
            {
                sizeField = value;
                RaisePropertyChanged("size");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string uptime
        {
            get => uptimeField;
            set
            {
                uptimeField = value;
                RaisePropertyChanged("uptime");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string refresh_rate
        {
            get => refresh_rateField;
            set
            {
                refresh_rateField = value;
                RaisePropertyChanged("refresh_rate");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string my_home
        {
            get => my_homeField;
            set
            {
                my_homeField = value;
                RaisePropertyChanged("my_home");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string limit
        {
            get => limitField;
            set
            {
                limitField = value;
                RaisePropertyChanged("limit");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string have_quota
        {
            get => have_quotaField;
            set
            {
                have_quotaField = value;
                RaisePropertyChanged("have_quota");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string isverbose
        {
            get => isverboseField;
            set
            {
                isverboseField = value;
                RaisePropertyChanged("isverbose");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string start
        {
            get => startField;
            set
            {
                startField = value;
                RaisePropertyChanged("start");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string finish
        {
            get => finishField;
            set
            {
                finishField = value;
                RaisePropertyChanged("finish");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string version
        {
            get => versionField;
            set
            {
                versionField = value;
                RaisePropertyChanged("version");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string new_rel_url
        {
            get => new_rel_urlField;
            set
            {
                new_rel_urlField = value;
                RaisePropertyChanged("new_rel_url");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string my_lcldata
        {
            get => my_lcldataField;
            set
            {
                my_lcldataField = value;
                RaisePropertyChanged("my_lcldata");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string color_scheme
        {
            get => color_schemeField;
            set
            {
                color_schemeField = value;
                RaisePropertyChanged("color_scheme");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string diskspacetotal1
        {
            get => diskspacetotal1Field;
            set
            {
                diskspacetotal1Field = value;
                RaisePropertyChanged("diskspacetotal1");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nt
        {
            get => ntField;
            set
            {
                ntField = value;
                RaisePropertyChanged("nt");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string status
        {
            get => statusField;
            set
            {
                statusField = value;
                RaisePropertyChanged("status");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string last_warning
        {
            get => last_warningField;
            set
            {
                last_warningField = value;
                RaisePropertyChanged("last_warning");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string have_warnings
        {
            get => have_warningsField;
            set
            {
                have_warningsField = value;
                RaisePropertyChanged("have_warnings");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cache_art
        {
            get => cache_artField;
            set
            {
                cache_artField = value;
                RaisePropertyChanged("cache_art");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string sizeleft
        {
            get => sizeleftField;
            set
            {
                sizeleftField = value;
                RaisePropertyChanged("sizeleft");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string finishaction
        {
            get => finishactionField;
            set
            {
                finishactionField = value;
                RaisePropertyChanged("finishaction");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string paused_all
        {
            get => paused_allField;
            set
            {
                paused_allField = value;
                RaisePropertyChanged("paused_all");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string quota
        {
            get => quotaField;
            set
            {
                quotaField = value;
                RaisePropertyChanged("quota");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string newzbin_url
        {
            get => newzbin_urlField;
            set
            {
                newzbin_urlField = value;
                RaisePropertyChanged("newzbin_url");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string new_release
        {
            get => new_releaseField;
            set
            {
                new_releaseField = value;
                RaisePropertyChanged("new_release");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string pause_int
        {
            get => pause_intField;
            set
            {
                pause_intField = value;
                RaisePropertyChanged("pause_int");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string mbleft
        {
            get => mbleftField;
            set
            {
                mbleftField = value;
                RaisePropertyChanged("mbleft");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string diskspace1
        {
            get => diskspace1Field;
            set
            {
                diskspace1Field = value;
                RaisePropertyChanged("diskspace1");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string scripts
        {
            get => scriptsField;
            set
            {
                scriptsField = value;
                RaisePropertyChanged("scripts");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string darwin
        {
            get => darwinField;
            set
            {
                darwinField = value;
                RaisePropertyChanged("darwin");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string timeleft
        {
            get => timeleftField;
            set
            {
                timeleftField = value;
                RaisePropertyChanged("timeleft");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string mb
        {
            get => mbField;
            set
            {
                mbField = value;
                RaisePropertyChanged("mb");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string noofslots
        {
            get => noofslotsField;
            set
            {
                noofslotsField = value;
                RaisePropertyChanged("noofslots");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string eta
        {
            get => etaField;
            set
            {
                etaField = value;
                RaisePropertyChanged("eta");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string diskspacetotal2
        {
            get => diskspacetotal2Field;
            set
            {
                diskspacetotal2Field = value;
                RaisePropertyChanged("diskspacetotal2");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string nzb_quota
        {
            get => nzb_quotaField;
            set
            {
                nzb_quotaField = value;
                RaisePropertyChanged("nzb_quota");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string loadavg
        {
            get => loadavgField;
            set
            {
                loadavgField = value;
                RaisePropertyChanged("loadavg");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cache_max
        {
            get => cache_maxField;
            set
            {
                cache_maxField = value;
                RaisePropertyChanged("cache_max");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string kbpersec
        {
            get => kbpersecField;
            set
            {
                kbpersecField = value;
                RaisePropertyChanged("kbpersec");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string speedlimit
        {
            get => speedlimitField;
            set
            {
                speedlimitField = value;
                RaisePropertyChanged("speedlimit");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string webdir
        {
            get => webdirField;
            set
            {
                webdirField = value;
                RaisePropertyChanged("webdir");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string queue_details
        {
            get => queue_detailsField;
            set
            {
                queue_detailsField = value;
                RaisePropertyChanged("queue_details");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string left_quota
        {
            get => left_quotaField;
            set
            {
                left_quotaField = value;
                RaisePropertyChanged("left_quota");
            }
        }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string diskspace2
        {
            get => diskspace2Field;
            set
            {
                diskspace2Field = value;
                RaisePropertyChanged("diskspace2");
            }
        }

        [XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [XmlArrayItemAttribute("slot", typeof(QueueSlotsSlot),
            Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public QueueSlotsSlot[] slots
        {
            get => slotsField;
            set
            {
                slotsField = value;
                RaisePropertyChanged("slots");
            }
        }

        [XmlElementAttribute("categories", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public QueueCategories[] categories
        {
            get => categoriesField;
            set
            {
                categoriesField = value;
                RaisePropertyChanged("categories");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
} // namespace

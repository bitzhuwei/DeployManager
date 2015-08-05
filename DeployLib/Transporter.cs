using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace DeployLib
{
    public class Transporter
    {
        private string _goods;
        private const string strGoods = "Goods";
        public string Goods
        {
            get { return _goods; }
            set { _goods = value; }
        }
        private string _ip;
        private const string strIp = "Ip";
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        private string _username;
        private const string strUsername = "Username";
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        private string _password;
        private const string strPassword = "Password";
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Ip))
            {
                return Goods;
            }
            else
            {
                return string.Format(@"\\{0}\{1}", Ip, Goods);
            }
        }

        public XElement ToXElement()
        {
            if (string.IsNullOrEmpty(Ip))
            {
                var result = new XElement(strTransporter,
                    new XElement(strGoods, Goods));
                return result;
            }
            else
            {
                var result = new XElement(strTransporter,
                    new XElement(strGoods, Goods),
                    new XElement(strIp, Ip),
                    new XElement(strUsername, Username),
                    new XElement(strPassword, Password));//Utility.Encrypt(Password)));
                return result;
            }
        }
        public static Transporter Parse(XElement xml)
        {
            if (xml == null || xml.Name != strTransporter) { return null; }
            var result = new Transporter();
            result.Goods = xml.Element(strGoods).Value;
            var ipNode = xml.Element(strIp);
            if (ipNode != null) { result.Ip = ipNode.Value; }
            var usernameNode = xml.Element(strUsername);
            if (usernameNode != null) { result.Username = usernameNode.Value; }
            var passwordNode = xml.Element(strPassword);
            if (passwordNode != null) { result.Password = passwordNode.Value; }//Utility.Decrypt(passwordNode.Value); }

            return result;
        }
        public const string strTransporter = "Transporter";
    }
}

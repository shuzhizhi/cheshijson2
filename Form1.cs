using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace cheshijs2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct ToJsonMy
        {
            public string solo_competitive_rank { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
            public string leaderboard_rank { get; set; }
            public string competitive_rank { get; set; }
            public profile profile;
           

        }
        public struct profile
        {
            public string solo_competitive_rank { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
            public string steamid { get; set; }
            public string personaname { get; set; }
          
           

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData("https://api.opendota.com/api/players/185763728"); //从指定网站下载数据
                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    
                JavaScriptSerializer js = new JavaScriptSerializer();   //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                ToJsonMy list = js.Deserialize<ToJsonMy>(pageHtml);
                string result = list.profile.personaname;
                textBox1.Text = result;
                textBox1.Text = textBox1.Text+"||"+ list.profile.steamid;
                //using (StreamWriter sw = new StreamWriter("c:\\test\\ouput.html"))//将获取的内容写入文本
                //{
                //    sw.Write(pageHtml);
                //}
                //Console.ReadLine(); //让控制台暂停,否则一闪而过了    
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }
        }
    }
}

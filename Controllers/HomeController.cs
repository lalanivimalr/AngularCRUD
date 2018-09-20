namespace AngularCRUD.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
			//return View();
        }
        //demo 3
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

namespace TreeViewDemo
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public class TaskManager
    {
        TreeView ObjTaskManager;
        public TaskManager(TreeView objTaskManager, int UserId = 0)
        {
            objTaskManager.NodeMouseClick += ObjTaskManager_NodeMouseClick;
            this.ObjTaskManager = objTaskManager;
            LoadTaskManager(UserId);
        }
        private void ObjTaskManager_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TaskNode m = (TaskNode)e.Node;
            // MessageBox.Show(m.Description);
        }
        private void LoadTaskManager(int UserId)
        {
            ObjTaskManager.Nodes.Clear();
            DataSet ds = SeedDataSet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    DataRow[] drAssigns = ds.Tables[1].Select("TaskID =" + dr["TaskID"].ToString());
                    TaskNode tn = TaskNode.CreateTaskNode(dr, drAssigns);

                    TreeNode[] trNs = ObjTaskManager.Nodes.Find(tn.ParentTaskId.ToString(), true);
                    if (trNs != null && trNs.Count() > 0)
                    {
                        trNs[0].Nodes.Add(tn);
                    }
                    else
                    {
                        ObjTaskManager.Nodes.Add(tn);
                    }
                }
            }
        }

        private DataSet SeedDataSet()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("TaskID");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Description");
            dt.Columns.Add("ParentTaskId");
            dt.Columns.Add("OwnerId");
            dt.Columns.Add("StatusPercentage");
            dt.Columns.Add("TargetDtTm");
            dt.Columns.Add("AssignDtTm");
            dt.Columns.Add("CompletionDtTm");
            dt.Columns.Add("StatusComment");

            for (int i = 1; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = i.ToString();
                dr["Subject"] = "Subject " + i.ToString();
                dr["Description"] = "Description " + i.ToString();
                dr["StatusComment"] = "StatusComment " + i.ToString();
                dr["OwnerId"] = "1";
                dr["StatusPercentage"] = "30.5";
                dr["TargetDtTm"] = DateTime.Now.ToString();
                dr["AssignDtTm"] = DateTime.Now.ToString();
                dr["CompletionDtTm"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }
            for (int i = 1; i <= 2; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = (3 + i).ToString();
                dr["Subject"] = "Subject " + (3 + i).ToString();
                dr["Description"] = "Description " + (3 + i).ToString();
                dr["StatusComment"] = "StatusComment " + (3 + i).ToString();
                dr["OwnerId"] = "1";
                dr["ParentTaskId"] = "1";
                dr["StatusPercentage"] = "30.5";
                dr["TargetDtTm"] = DateTime.Now.ToString();
                dr["AssignDtTm"] = DateTime.Now.ToString();
                dr["CompletionDtTm"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }
            for (int i = 1; i <= 2; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = (5 + i).ToString();
                dr["Subject"] = "Subject " + (5 + i).ToString();
                dr["Description"] = "Description " + (5 + i).ToString();
                dr["StatusComment"] = "StatusComment " + (5 + i).ToString();
                dr["OwnerId"] = "1";
                dr["ParentTaskId"] = "2";
                dr["StatusPercentage"] = "30.5";
                dr["TargetDtTm"] = DateTime.Now.ToString();
                dr["AssignDtTm"] = DateTime.Now.ToString();
                dr["CompletionDtTm"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }
            for (int i = 1; i <= 2; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = (7 + i).ToString();
                dr["Subject"] = "Subject " + (7 + i).ToString();
                dr["Description"] = "Description " + (7 + i).ToString();
                dr["StatusComment"] = "StatusComment " + (7 + i).ToString();
                dr["OwnerId"] = "1";
                dr["ParentTaskId"] = "3";
                dr["StatusPercentage"] = "30.5";
                dr["TargetDtTm"] = DateTime.Now.ToString();
                dr["AssignDtTm"] = DateTime.Now.ToString();
                dr["CompletionDtTm"] = DateTime.Now.ToString();
                dt.Rows.Add(dr);
            }

            dt.TableName = "TaskMst";
            ds.Tables.Add(dt.Copy());

            dt = new DataTable();
            dt.Columns.Add("TaskID");
            dt.Columns.Add("UserName");
            dt.Columns.Add("UserId");
            dt.TableName = "TaskChld";
            for (int i = 4; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = i.ToString();
                dr["UserId"] = "1";
                dr["UserName"] = "UserName 1";
                dt.Rows.Add(dr);
            }
            for (int i = 4; i < 8; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TaskID"] = i.ToString();
                dr["UserId"] = "2";
                dr["UserName"] = "UserName 2";
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt.Copy());
            return ds;
        }
    }
    public class TaskNode : TreeNode
    {
        public string Description { get; set; }
        public int ParentTaskId { get; set; }
        public int OwnerId { get; set; }
        public List<int> UserId { get; set; }
        public List<string> UserName { get; set; }
        public double StatusPercentage { get; set; }
        public string StatusComment { get; set; }
        public DateTime CompletionDtTm { get; set; }
        public DateTime TargetDtTm { get; set; }
        public DateTime AssignDtTm { get; set; }
        public static TaskNode CreateTaskNode(DataRow dr, DataRow[] drAssigns)
        {
            TaskNode tn = new TaskNode();
            if (dr != null)
            {
                tn.Name = dr["TaskID"].ToString();
                tn.Tag = dr["TaskID"].ToString();
                tn.Text = dr["Subject"].ToString();
                tn.Description = dr["Description"].ToString();
                if (dr["ParentTaskId"] != DBNull.Value)
                    tn.ParentTaskId = Convert.ToInt32(dr["ParentTaskId"]);
                if (dr["OwnerId"] != DBNull.Value)
                    tn.OwnerId = Convert.ToInt32(dr["OwnerId"]);
                if (dr["StatusPercentage"] != DBNull.Value)
                    tn.StatusPercentage = Convert.ToDouble(dr["StatusPercentage"]);
                if (dr["StatusComment"] != DBNull.Value)
                    tn.StatusComment = dr["StatusComment"].ToString();
                if (dr["TargetDtTm"] != DBNull.Value)
                    tn.TargetDtTm = Convert.ToDateTime(dr["TargetDtTm"]);
                if (dr["AssignDtTm"] != DBNull.Value)
                    tn.AssignDtTm = Convert.ToDateTime(dr["AssignDtTm"]);
                if (dr["CompletionDtTm"] != DBNull.Value)
                    tn.CompletionDtTm = Convert.ToDateTime(dr["CompletionDtTm"]);

                if (drAssigns != null && drAssigns.Count() > 0)
                {
                    tn.UserId = new List<int>();
                    tn.UserName = new List<string>();
                    for (int j = 0; j < drAssigns.Count(); j++)
                    {
                        if (drAssigns[j]["UserId"] != DBNull.Value)
                            tn.UserId.Add(Convert.ToInt32(drAssigns[j]["UserId"]));
                        tn.UserName.Add(drAssigns[j]["UserName"].ToString());
                    }
                }

            }
            return tn;
        }
    }
}

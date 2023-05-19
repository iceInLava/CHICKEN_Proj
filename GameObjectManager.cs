using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 只因了个只因
{
    internal class GameObjectManager
    {
        public int grid_x_count;
        public int grid_y_count;
        public int grid_z_count = 3;

        /// <summary>
        /// 包含所有Button，统一管理
        /// </summary>
        public static List<MyButton> button_list_1;
        public static List<MyButton> button_list_2;
        public static List<MyButton> button_list_3;

        public static List<MyButton> button_list_clicked;
        /// <summary>
        /// button 的个数
        /// 你最好用18的倍数（恼）
        /// </summary>
        public static int button_count;
        
        
        

        public GameObjectManager() 
        {
            button_list_1 = new List<MyButton>();
            button_list_2 = new List<MyButton>();
            button_list_3 = new List<MyButton>();
            button_count = 108;
            grid_x_count = 9;
            grid_y_count = 9;
            grid_z_count = 3;
            //threeDGrid = new int[6, 6, 3];

            button_list_clicked = new List<MyButton>();
        }
        /// <summary>
        /// 初始化buttons
        /// 思路为:将buttons分别放在3个List中，表示上中下三层的层级关系
        /// 随机将button初始化为六种button，分别装在三个链表中
        /// </summary>
        public void Init_buttons()
        {
            Random random = new Random();
            int[] check_btn_type = new int[6];
            for(int i = 0; i < button_count; )
            {
                int type = random.Next(0,6);
                if (check_btn_type[type] < button_count / 6)
                {
                    check_btn_type[type]++;
                    MyButton btn = new MyButton(50, 50, type);
                    btn.BackgroundImage = Image.FromFile(btn.URL);
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.btn_index = i;
                    if(i < button_count/3)
                    {
                        button_list_1.Add(btn);
                        i++;
                        continue;
                    }else if(i < button_count * 2 / 3)
                    {
                        button_list_2.Add(btn);
                        i++;
                        continue;
                    }else
                    {
                        button_list_3.Add(btn);
                        i++;
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// 显示buttons（调用Put_button()）
        /// </summary>
        /// <param name="game">当前游戏的GameManager</param>
        public void Show_buttons(GameObjectManager game)
        {
            Put_button(game, button_list_1, 1);
            Put_button(game, button_list_2, 2);
            Put_button(game, button_list_3, 3);
        }
        /// <summary>
        /// 在窗体内生成某一层的button
        /// </summary>
        /// <param name="game">当前游戏的GameManager</param>
        /// <param name="button_list">某一层的buttonList</param>
        /// <param name="layer">第几层</param>
        protected void Put_button(GameObjectManager game, List<MyButton> button_list, int layer)
        {
            Random random = new Random();
            HashSet<int> selectedIndexs = new HashSet<int>();

            foreach (var btn in button_list)
            {
                int index = random.Next(0, game.grid_x_count * game.grid_y_count);
                if (selectedIndexs.Contains(index) == false)
                {
                    btn.grid_x_btn = index / game.grid_x_count;
                    btn.grid_y_btn = index % game.grid_y_count;
                    btn.grid_z_btn = layer;
                    btn.Location = new Point(btn.grid_x_btn * 65 + btn.grid_z_btn*10,
                        btn.grid_y_btn * 65 + btn.grid_z_btn*10);
                    Form.ActiveForm.Controls.Add(btn);
                }
            }
            selectedIndexs.Clear();
        }
        /// <summary>
        /// 判断button是否Enabled
        /// </summary>
        public static void Active_btn()
        {
            foreach(var middle in button_list_2)
            {
                middle.Enabled = !IsButtonCovered(middle, button_list_1);
            }
            foreach(var lower in button_list_3)
            {
                lower.Enabled = !(IsButtonCovered(lower, button_list_2) 
                    || IsButtonCovered(lower, button_list_1));
            }
        }
        /// <summary>
        /// （被Active_btn()调用）遍历下层button
        /// 列表，判断button是否被其他button覆盖
        /// </summary>
        /// <param name="lowerBtn"></param>
        /// <param name="btn_list"></param>
        /// <returns>被盖住：true，没盖住：false </returns>
        protected static bool IsButtonCovered(MyButton lowerBtn, List<MyButton> btn_list)
        {
            if (btn_list.Count == 0)
                return false;
            // 获取上层按钮和下层按钮的位置和尺寸
            Rectangle lowerBounds = lowerBtn.Bounds;
            foreach(var btn in btn_list)
            {
               Rectangle upperBounds = btn.Bounds;
               Rectangle intersection = Rectangle.Intersect(upperBounds, lowerBounds);
                if (intersection.IsEmpty == false && lowerBtn.Visible && btn.Visible)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="key"></param>
        public static void Click_btn_to_result(int key)
        {
            MyButton newButton = new MyButton();
            newButton.BackgroundImageLayout = ImageLayout.Zoom;
            
            if (key < button_count/3)
            {
                //button_list_clicked.Add(button_list_1[key]);K?
                newButton.URL = button_list_1[key].URL;
                newButton.tag = button_list_1[key].tag;
                newButton.BackgroundImage = Image.FromFile(newButton.URL);
                newButton.Size = button_list_1[key].Size;
            }
            else if(key < button_count*2/3)
            {
                //button_list_clicked.Add(button_list_2[key%(button_count/3)]);
                newButton.URL = button_list_2[key - (button_count / 3)].URL;
                newButton.tag = button_list_2[key - (button_count / 3)].tag;
                newButton.BackgroundImage = Image.FromFile(newButton.URL);
                newButton.Size = button_list_2[key - (button_count / 3)].Size;
            }
            else
            {
                //button_list_clicked.Add(button_list_3[key%(button_count*2/3)]);
                newButton.URL = button_list_3[key - (button_count * 2 / 3)].URL;
                newButton.tag = button_list_3[key - (button_count * 2 / 3)].tag;
                newButton.BackgroundImage = Image.FromFile(newButton.URL);
                newButton.Size = button_list_3[key - (button_count * 2 / 3)].Size;
            }
            button_list_clicked.Add(newButton);
            Form form = Form.ActiveForm;
            FlowLayoutPanel result_panel = form.Controls.Find("panel_result", false).First() as FlowLayoutPanel;

            result_panel.Controls.Clear();

            //result_panel.Dock = DockStyle.Fill; // 设置 FlowLayoutPanel 填充 Panel
            result_panel.FlowDirection = FlowDirection.LeftToRight; // 设置按钮从左到右排列
            result_panel.WrapContents = false; // 不换行

            foreach(var btn in button_list_clicked)
            {
                btn.Visible = true;
                btn.Enabled = false;
                result_panel.Controls.Add(btn);
            }
            form.Refresh();
        }
        /// <summary>
        /// 判断是否三消，如果暂存button的栏满了会弹窗：“你失败了！爬！”
        /// </summary>
        public static void Check_clean_or_lose()
        {
            string clean_name = null;
            if(button_list_clicked.Count > 6)
            {
                MessageBox.Show("你失败了！爬！");
            }
            Dictionary<string, int> dict_tag_frequency = new Dictionary<string, int>
            {
                {"doge", 0 },
                {"hj", 0 },
                {"lh", 0 },
                {"lyj", 0 },
                {"miao", 0 },
                {"zh", 0 }
            };
            foreach (var btn in button_list_clicked)
            {
                dict_tag_frequency[btn.tag] += 1;
            }
            foreach(KeyValuePair<string, int> kv in dict_tag_frequency)
            {
                if(kv.Value >= 3)
                {
                    //MessageBox.Show("3");
                    dict_tag_frequency[kv.Key] = 0;
                    clean_name = kv.Key;
                    Form form = Form.ActiveForm;
                    FlowLayoutPanel flp = form.Controls.Find("panel_result", false).First()as FlowLayoutPanel;
                    //MessageBox.Show("删了3");
                    MyButton[] del_buttons = new MyButton[3];
                    int index = 0;
                    for(int i = 0; i < button_list_clicked.Count; i++ )
                    {
                        if (button_list_clicked[i].tag == kv.Key)
                        {
                            del_buttons[index] = button_list_clicked[i];
                            index++;
                        }
                        if (index == 3)
                            break;
                    }
                    button_list_clicked.RemoveAll(btn => btn.tag == kv.Key);

                    for(int i =0; i<del_buttons.Count(); i++)
                    {
                        flp.Controls.Remove(del_buttons[i]);
                    }
                    

                    break;
                }
            }
        }
    }
}

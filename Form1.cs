namespace 只因了个只因
{
    public partial class Form1 : Form
    {
        GameObjectManager game;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game = new GameObjectManager();

            game.Init_buttons();
            game.Show_buttons(game);
            GameObjectManager.Active_btn();
            button1.Enabled = false;
        }
    }
}
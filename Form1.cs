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
            //MyButton myButton = new MyButton();
            //myButton.Location = new Point(100, 100);
            //myButton.Image = Image.FromFile(myButton.URL);
            //Controls.Add(myButton);

            game = new GameObjectManager();

            game.Init_buttons();
            game.ShowButtons(game);
            GameObjectManager.Active_btn();
            button1.Enabled = false;
        }
    }
}
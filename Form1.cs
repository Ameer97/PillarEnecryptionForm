using System;
using System.Configuration;

namespace PillarEnecryptionForm
{
    public partial class Form1 : Form
    {
        uint p = 0;
        uint q = 0;
        uint n = 0;
        uint n_Sq = 0;
        uint np = 0;
        bool gcdResult = false;
        uint lemda = 0;
        uint g = 0;
        uint l = 0;
        uint mmi = 0;
        uint cipher = 0;
        uint dcrypt = 0;
        private PaillierCryptosystemDto Keys { get; set; }
        public Form1()
        {
            InitializeComponent();

            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox8.Enabled = false;
            textBox11.Enabled = false;
            textBox13.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p = uint.Parse(textBox1.Text);
            q = uint.Parse(textBox2.Text);

            n = (p * q);
            textBox3.Text = n.ToString();

            n_Sq = n * n;
            textBox4.Text = n_Sq.ToString();

            gcdResult = Gcd(n, ((p - 1) * (q - 1))) == 1;
            if (!gcdResult)
            {
                textBox5.Text = "Not Valid";
                return;
            }

            textBox5.Text = "Valid P & Q";
            lemda = Lcm(p - 1, q - 1);
            textBox6.Text = lemda.ToString();

            g = uint.Parse(textBox7.Text);
            if (g != 4886) g = (uint)new Random().Next(2, (int)n_Sq - 1);
            textBox7.Text = g.ToString();

            var x = pow(g, lemda, n_Sq);
            var x1 = L(x, n);
            var x2 = inv(x1, n);
            var u = x2 % n;

            textBox8.Text = u.ToString();

            Keys = new PaillierCryptosystemDto(u, lemda, n, g);

        }

        public void PaillierCryptosystem(uint p = 13, uint q = 17, uint g = 4886)
        {
            var random = new Random();
            n = (p * q);
            n_Sq = n * n;
            gcdResult = Gcd(n, ((p - 1) * (q - 1))) == 1;
            if (gcdResult)
            {
                lemda = Lcm(p - 1, q - 1);

                if (g != 4886) g = (uint)random.Next(2, (int)n_Sq - 1);

                var x = pow(g, lemda, n_Sq);
                var x1 = L(x, n);
                var x2 = inv(x1, n);
                var u = x2 % n;
                Keys = new PaillierCryptosystemDto(u, lemda, n, g);
                return;
            }
            throw new Exception("Invalid Ouiler, please choose another (p, q)");
        }
        private uint Gcd(uint a, uint b)
        {
            if (b == 0) return a;
            return Gcd(b, a % b);
        }
        private uint Lcm(uint a, uint b)
        {
            return (a * b) / Gcd(a, b);
        }
        private uint L(uint x, uint n)
        {
            return ((x - 1) / n);
        }
        private uint inv(uint x, uint n, uint b = 1)
        {
            if ((x * b) % n == 1) return b;
            return inv(x, n, b + 1);
        }

        private uint pow(uint x, uint mod, uint n)
        {
            uint result = 1;
            for (uint i = mod; i > 0; i--)
            {
                result = (result * x) % n;
            }
            return result;
        }

        public uint Dcrypt(uint cipher)
        {
            return (L(pow(cipher, Keys.Lamda, Keys.ModuleSqure), Keys.Module) * Keys.Uoiler) % Keys.Module;
        }

        public uint Encrypt(uint message = 123, uint r = 59)
        {
            return (pow(Keys.G, message, Keys.ModuleSqure) * pow(r, Keys.Module, Keys.ModuleSqure)) % Keys.ModuleSqure;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var message = uint.Parse(textBox9.Text);
            var r = uint.Parse(textBox10.Text);
            cipher = Encrypt(message, r);
            textBox11.Text = cipher.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cipher = uint.Parse(textBox12.Text);
            dcrypt = Dcrypt(cipher);

            textBox13.Text = dcrypt.ToString();

        }
    }
}
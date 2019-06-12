using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Agenda_API_View.Models;

namespace Agenda_API_View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        string URI = "";
        int codigoProduto = 1;

        private void btnGetApi_Click(object sender, EventArgs e)
        {
            GetContact(Convert.ToInt32(txtId.Text));
        }

        private async void GetAllContacts()
        {
            listBoxAgenda.Items.Clear();
            URI = "https://localhost:44333/api/values/";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ContatosJsonString = await response.Content.ReadAsStringAsync();
                        var dados = JsonConvert.DeserializeObject<Agenda[]>(ContatosJsonString).ToList();

                        if (dados != null)
                        {
                            foreach (var item in dados)
                            {
                                listBoxAgenda.Items.Add("ID: " + item.ID + "  ||  NOME: " + item.Nome);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter a agenda : " + response.StatusCode);
                    }
                }
            }
        }

        private async void GetContact(int id)
        {
            URI = "https://localhost:44333/api/values/" + id;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ContatosJsonString = await response.Content.ReadAsStringAsync();
                        var dados = JsonConvert.DeserializeObject<Agenda>(ContatosJsonString);

                        if (dados != null)
                        {
                            txtNome.Text = dados.Nome;
                            txtTelefone.Text = dados.Telefone;
                            txtEndereco.Text = dados.Endereco;
                        }
                        else
                        {
                            MessageBox.Show(null, "Contato não encontrado!", "Atenção!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível obter a agenda : " + response.StatusCode);
                    }
                }
            }
        }

        private async void AddContact()
        {
            URI = "https://localhost:44333/api/values";
            Agenda contato = new Agenda();
            contato.Nome = txtNome.Text;
            contato.Telefone = txtTelefone.Text;
            contato.Endereco = txtEndereco.Text;

            using (var client = new HttpClient())
            {
                var serializedProduto = JsonConvert.SerializeObject(contato);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
            GetAllContacts();
        }

        private async void DeleteProduto()
        {
            var contatoID = txtId.Text;
            URI = "https://localhost:44333/api/values/" + contatoID;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                HttpResponseMessage responseMessage = await
                    client.DeleteAsync(URI);
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Produto excluído com sucesso");
                }
                else
                {
                    MessageBox.Show("Falha ao excluir o produto  : " + responseMessage.StatusCode);
                }
            }
            GetAllContacts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllContacts();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DeleteProduto();
        }
    }
}

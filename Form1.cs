using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRUD_MG
{
    public partial class AgendaContato : Form
    {
        string connectionString = "Server=localhost;Database=serverBanco;Uid=root;Pwd=1911@JlPh;";
        bool novo;

        public AgendaContato()
        {
            InitializeComponent();
        }

        private void AgendaContato_Load(object sender, EventArgs e)
        {
            btn_Inserir.Enabled = true;
            btn_Editar.Enabled = false;
            btn_Remover.Enabled = false;
            btn_Consutar.Enabled = true;
            btn_Salvar.Enabled = false;
            btn_Cancelar.Enabled = true;
            txtNome.Enabled = true;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
        }

        /* Botão Inserir */
        private void btn_Inserir_Click(object sender, EventArgs e)
        {
            btn_Inserir.Enabled = false;
            btn_Editar.Enabled = true;
            btn_Remover.Enabled = false;
            btn_Consutar.Enabled = false;
            btn_Salvar.Enabled = true;
            btn_Cancelar.Enabled = true;
            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtNome.Focus();
            novo = true;
        }

        /* Botão Editar */
        private void btn_Editar_Click(object sender, EventArgs e)
        {
            btn_Inserir.Enabled = false;
            btn_Editar.Enabled = false;
            btn_Remover.Enabled = false;
            btn_Consutar.Enabled = false;
            btn_Salvar.Enabled = true;
            btn_Cancelar.Enabled = true;
            txtNome.Enabled = false;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            novo = false;
        }

        /* Botão Salvar */
        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (novo)
                {
                    string sql = "INSERT INTO CLIENTE (NOME, EMAIL, TELEFONE) VALUES (@Nome, @Email, @Telefone)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                                MessageBox.Show("Cadastro salvo com sucesso");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                    }
                }
                else
                {
                    string sql = "UPDATE CLIENTE SET NOME=@Nome, EMAIL=@Email, TELEFONE=@Telefone WHERE NOME=@OldNome";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                        cmd.Parameters.AddWithValue("@OldNome", txtNome.Text);

                        try
                        {
                            int linhasAfetadas = cmd.ExecuteNonQuery();
                            if (linhasAfetadas > 0)
                                MessageBox.Show("Cadastro atualizado com sucesso");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    btn_Inserir.Enabled = true;
                    btn_Editar.Enabled = false;
                    btn_Remover.Enabled = false;
                    btn_Consutar.Enabled = true;
                    btn_Salvar.Enabled = false;
                    btn_Cancelar.Enabled = false;
                    txtNome.Enabled = true;
                    txtEmail.Enabled = false;
                    txtTelefone.Enabled = false;
                }
            }
        }

        /* Botão Cancelar */
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            btn_Inserir.Enabled = true;
            btn_Editar.Enabled = false;
            btn_Remover.Enabled = false;
            btn_Consutar.Enabled = false;
            btn_Salvar.Enabled = false;
            btn_Cancelar.Enabled = false;
            txtNome.Enabled = true;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }

        /* Botão Remover */
        private void btn_Remover_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM CLIENTE WHERE NOME=@Nome";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Registro excluído");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }

            btn_Inserir.Enabled = true;
            btn_Editar.Enabled = false;
            btn_Remover.Enabled = false;
            btn_Consutar.Enabled = true;
            btn_Salvar.Enabled = false;
            btn_Cancelar.Enabled = false;
            txtNome.Enabled = true;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }

        /* Botão Consutar*/
        private void btn_Consutar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT*FROM CLIENTE WHERE NOME=" + txtNome.Text;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand (sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    btn_Inserir.Enabled = false;
                    btn_Editar.Enabled = true;
                    btn_Remover.Enabled = true;
                    btn_Consutar.Enabled = false;
                    btn_Salvar.Enabled = true;
                    btn_Cancelar.Enabled = true;
                    txtNome.Enabled = true;
                    txtEmail.Enabled = true;
                    txtTelefone.Enabled = true;
                    txtNome.Focus();
                    txtNome.Text = reader[0].ToString();
                    txtEmail.Text = reader[1].ToString();
                    txtTelefone.Text = reader[2].ToString();
                    novo = false;
                }
                else
                    MessageBox.Show("Nenhum registro encontrado com o Nome informado!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }

            txtNome.Text = "";

            reader = cmd.ExecuteReader ();
        }
    }
}

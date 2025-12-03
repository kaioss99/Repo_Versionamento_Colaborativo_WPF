using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppAgendaDeTarefas;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string descricao;
    private int i;
    private List<String> listaTarefas = new List<String>();

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// funcionalidade inicial 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Adicionar_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TxtTarefa.Text))
        {
            ListaTarefas.Items.Add(TxtTarefa.Text);
            AdicionarTarefas();
            TxtTarefa.Clear();
            TxtTarefa.Focus();
        }
        else
        {
            MessageBox.Show("Digite uma tarefa válida!");
        }

        ContadorTarefa();
    }

    private void Limpar_lista_Click(object sender, RoutedEventArgs e)
    {
        if (ListaTarefas.Items.Count > 0)
        {
            ListaTarefas.Items.Clear();

            MessageBox.Show("A lista foi limpa com sucesso!", "Limpeza");
        }

        TbxContadorTarefas.Text = String.Empty;
    }

    private void AdicionarTarefas()
    {
        descricao = TxtTarefa.Text.Trim();

        if (!string.IsNullOrWhiteSpace(descricao))
        {
            listaTarefas.Add(descricao);
        }
    }

    private void Editartarefa_OnClick(object sender, RoutedEventArgs e)
    {
        // Verifica se existe uma tarefa selecionada
        if (ListaTarefas.SelectedItem != null)
        {
            // VALIDAÇÃO ADICIONADA: Verifica se o campo de texto não está vazio
            if (string.IsNullOrWhiteSpace(TxtTarefa.Text))
            {
                MessageBox.Show("Digite um texto válido para editar a tarefa!", "Campo Vazio");
                return; // Sai do método se o campo estiver vazio
            }

            // Pega o índice do item selecionado
            int index = ListaTarefas.SelectedIndex;

            // Edita a tarefa no ListBox usando o texto do TextBox de edição
            ListaTarefas.Items[index] = TxtTarefa.Text;

            MessageBox.Show("Tarefa editada com sucesso!");
        }
        else
        {
            MessageBox.Show("Selecione uma tarefa para editar!");
        }
    }

    //Botão de duplicar tarefa selecionada
    private void BtnDuplicar(object sender, RoutedEventArgs e)
    {
        if (ListaTarefas.SelectedItem != null)
        {
            string duplicata = ListaTarefas.SelectedItem.ToString();
            ListaTarefas.Items.Add(duplicata);

            MessageBox.Show("Tarefa Duplicada com sucesso!", "Duplicar", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Selecione uma tarefa para duplicar!", "Duplicar", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        ContadorTarefa();
    }

    private void ContadorTarefa()
    {
        for (i = 1; i <= ListaTarefas.Items.Count; i++)
        {
            TbxContadorTarefas.Text = i.ToString();
        }
    }

    private void BtnMoverTarefaUp_Click(object sender, RoutedEventArgs e)
    {
        // Obtem o índice da tarefa selecionada.
        int selectedIndex = ListaTarefas.SelectedIndex;

        //Verifica se não existe nenhuma tarefa selecionada.
        if (selectedIndex == -1)
        {
            MessageBox.Show("Selecione uma tarefa para movimentá-la!",
                "Mover", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        //Verifica se a tarefa já está no topo
        if (selectedIndex == 0)
        {
            MessageBox.Show("A tarefa já está posicionada no topo da lista!",
                "Mover", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string tarefaParaMover = ListaTarefas.SelectedItem as string;

        // Remove a tarefa da posição atual
        ListaTarefas.Items.RemoveAt(selectedIndex);

        // Insere a tarefa na posição anterior
        ListaTarefas.Items.Insert(selectedIndex - 1, tarefaParaMover);

        // Atualiza a seleção para o novo índice
        ListaTarefas.SelectedIndex = selectedIndex - 1;

        MessageBox.Show("Tarefa movida para cima com sucesso!",
            "Mover", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void BtnMoverTarefaDown_Click(object sender, RoutedEventArgs e)
    {
        // Obtem o índice da tarefa selecionada.
        int selectedIndex = ListaTarefas.SelectedIndex;

        //Verifica se não existe nenhuma tarefa selecionada.
        if (selectedIndex == -1)
        {
            MessageBox.Show("Selecione uma tarefa para movimentá-la!",
                "Mover", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        //Verifica se a tarefa já está no final da lista
        if (selectedIndex == ListaTarefas.Items.Count - 1)
        {
            MessageBox.Show("A tarefa já está posicionada no final da lista!",
                "Mover", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }


        string tarefaParaMover = ListaTarefas.SelectedItem as string;

        // Remove a tarefa da posição atual
        ListaTarefas.Items.RemoveAt(selectedIndex);

        // Insere a tarefa na posição anterior
        ListaTarefas.Items.Insert(selectedIndex + 1, tarefaParaMover);

        // Atualiza a seleção para o novo índice
        ListaTarefas.SelectedIndex = selectedIndex + 1;

        MessageBox.Show("Tarefa movida para baixo com sucesso!",
            "Mover", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void BtnConcluida_OnClick(object sender, RoutedEventArgs e)
    {
        if (ListaTarefas.SelectedItem != null)
        {
            int index = ListaTarefas.SelectedIndex;
            string selecao = ListaTarefas.SelectedItem.ToString();

            if (!selecao.EndsWith("✅"))
            {
                selecao += "✅";
                MessageBox.Show($"Status da tarefa: Concluida!",
                    "Gerenciador de Tarefas", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            ListaTarefas.Items[index] = selecao;
        }
    }
}
using RLDeberSemana5.Modelos;

namespace RLDeberSemana5.Vistas;

public partial class VistaDatos : ContentPage
{
	public VistaDatos()
	{
		InitializeComponent();
	}
    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "";
        App.PersonR.AddNewPerson(txtNombre.Text);
        StatusMessage.Text = App.PersonR.StatusMessage;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "";
        List<Persona> lista = App.PersonR.GetAllPeople();
        listaPersonas.ItemsSource = lista;
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";
        int id = int.Parse(txtID.Text);
        if (txtID.Text.Equals("") == false && id > 0)
        {
            App.PersonR.DeletePerson(id);
            RefreshCollectionView();
            lblStatusMessage.Text = "Dato Eliminado.";
        }
        else
        {
            lblStatusMessage.Text = "ingrese ID de persona para eliminar.";
        }
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";
        int id = int.Parse(txtID.Text);

        if (id > 0)
        {
            string updatedName = txtNombre.Text;

            Persona updatedPerson = new Persona
            {
                Id = id,
                Name = updatedName
            };

            App.PersonR.UpdatePerson(updatedPerson);
            RefreshCollectionView();
            lblStatusMessage.Text = "Dato actualizado.";
        }
        else
        {
            lblStatusMessage.Text = "Ingrese id de una persona para actualizar.";
        }

    }

    private  void RefreshCollectionView()
    {

        List<Persona> lista = App.PersonR.GetAllPeople();
        listaPersonas.ItemsSource = lista;
    }
    
}
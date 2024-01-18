using aaguirreS5.Models;

namespace aaguirreS5.Views;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        statusMessage.Text = App.personRepo.StatusMessage;

    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        if (ListaPersona.SelectedItem is Persona selectedPerson)
        {
            // Muestra los detalles de la persona seleccionada (puedes imprimir en la consola, mostrar en un mensaje, etc.)
            Console.WriteLine($"Persona seleccionada: Id = {selectedPerson.Id}, Name = {selectedPerson.Name}");
        }
        List<Persona> pers = App.personRepo.GetAllPorle();
        ListaPersona.ItemsSource=pers;
    }


    private bool actualizacionRealizada = false;



    private async void btnActualizar_Clicked_1(object sender, EventArgs e)
    {
        // Limpia el mensaje de estado
        statusMessage.Text = "";

        // Verifica si ya se ha realizado una actualizaci�n y si hay una persona seleccionada en la CollectionView
        if (!actualizacionRealizada)
        {
            if (ListaPersona.SelectedItem is Persona selectedPerson)
            {
                // Muestra un cuadro de di�logo solicitando al usuario que ingrese el nuevo nombre
                string newName = await DisplayPromptAsync("Actualizar", "Ingrese el nuevo nombre", "Aceptar", "Cancelar", "Nuevo Nombre");

                // Verifica si el usuario ingres� un nuevo nombre
                if (!string.IsNullOrEmpty(newName))
                {
                    // Actualiza la informaci�n de la persona seleccionada utilizando el m�todo UpdatePerson de PersonRepository
                    App.personRepo.UpdatePerson(selectedPerson.Id, newName);

                    // Actualiza el mensaje de estado con el resultado de la operaci�n de actualizaci�n
                    statusMessage.Text = App.personRepo.StatusMessage;

                    // Actualiza la lista de personas despu�s de la operaci�n de actualizaci�n
                    btnMostrar_Clicked(sender, e);
                }
            }
            else
            {
                // Si no hay una persona seleccionada, muestra un mensaje indicando que se debe seleccionar una persona para actualizar
                statusMessage.Text = "Seleccione una persona para actualizar";
            }

            // Marca que la actualizaci�n ha sido realizada
            actualizacionRealizada = true;
        }
        else
        {
            // Si ya se realiz� una actualizaci�n, muestra un mensaje indicando que se debe seleccionar una persona para actualizar
            statusMessage.Text = "Seleccione una persona para actualizar";

            // Reinicia la variable para permitir futuras actualizaciones
            actualizacionRealizada = false;
        }
    }

    private bool eliminacionRealizada = false;

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Limpia el mensaje de estado
            statusMessage.Text = "";

            // Verifica si ya se ha realizado una eliminaci�n y si hay una persona seleccionada en la CollectionView
            if (!eliminacionRealizada)
            {
                if (ListaPersona.SelectedItem is Persona selectedPerson)
                {
                    // Pregunta al usuario para confirmar la eliminaci�n
                    bool confirmacion = await DisplayAlert("Eliminar", $"�Est� seguro de eliminar a {selectedPerson.Name}?", "S�", "No");

                    // Si el usuario confirma la eliminaci�n, procede
                    if (confirmacion)
                    {
                        // Elimina la persona utilizando el m�todo DeletePerson de PersonRepository
                        App.personRepo.DeletePerson(selectedPerson.Id);

                        // Actualiza el mensaje de estado con el resultado de la operaci�n de eliminaci�n
                        statusMessage.Text = App.personRepo.StatusMessage;

                        // Actualiza la lista de personas despu�s de la operaci�n de eliminaci�n
                        btnMostrar_Clicked(sender, e);
                    }
                }
                else
                {
                    // Si no hay una persona seleccionada, muestra un mensaje indicando que se debe seleccionar una persona para eliminar
                    statusMessage.Text = "Seleccione una persona para eliminar";
                }

                // Marca que la eliminaci�n ha sido realizada
                eliminacionRealizada = true;
            }
            else
            {
                // Si ya se realiz� una eliminaci�n, muestra un mensaje indicando que se debe seleccionar una persona para eliminar
                statusMessage.Text = "Seleccione una persona para eliminar";

                // Reinicia la variable para permitir futuras eliminaciones
                eliminacionRealizada = false;
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: Muestra un mensaje en caso de error
            statusMessage.Text = $"Error: {ex.Message}";
        }
    }


}
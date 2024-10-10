namespace flapbird;

public partial class MainPage : ContentPage
{
	const int gravidade = 1;
	const int tempoentreframes = 25;
	bool estaMorto = false;
	
	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade(){
		Bird.TranslationY += gravidade;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenha();
    }
	async Task Desenha(){
		while(!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoentreframes);
		}
	}
	void OnGameOverCliked(object s,TappedEventArgs a){
		FrameGameOver.IsVisible= false;
		Inicializar();
		Desenha();
	}
	void Inicializar(){
		Bird.TranslationY=0;
	}
}


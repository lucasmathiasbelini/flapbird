namespace flapbird;

public partial class MainPage : ContentPage
{
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;


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
			GerenciarCanos();
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
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		larguraJanela = width;
		alturaJanela = height;
    }
	void GerenciarCanos(){
		TopPipe.TranslationX -= velocidade;
		BottomPipe.TranslationX -= velocidade;
		if(BottomPipe.TranslationX <- larguraJanela){
			BottomPipe.TranslationX = 13;
			TopPipe.TranslationX = 13;

		}

	}

}


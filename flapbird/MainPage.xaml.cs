namespace flapbird;

public partial class MainPage : ContentPage
{
    double larguraJanela = 0;
    double alturaJanela = 0;
    int velocidade = 20;

    const int gravidade = 1;
    const int tempoentreframes = 25;
    bool estaMorto = false;

    const int forcaPulo = 30;
    const int maxTempoPulando = 3;

    bool estaPulando = false;
    int TempoPulando = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    void AplicaGravidade()
    {
        Bird.TranslationY += gravidade;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Inicializar();  
        Desenha();
    }

    async Task Desenha()
    {
        while (!estaMorto)
        {
            if (estaPulando)
            {
                AplicaPulo();
            }
            else
            {
                AplicaGravidade();
            }

            await Task.Delay(tempoentreframes);
            GerenciarCanos();
        }
    }

    void OnGameOverCliked(object s, TappedEventArgs a)
    {
        GameOverGrid.IsVisible = false;
        Inicializar();
        Desenha();
    }

    void Inicializar()
    {
        Bird.TranslationY = 0;
        estaMorto = false;  
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        larguraJanela = width;
        alturaJanela = height;
    }

    void GerenciarCanos()
    {
        TopPipe.TranslationX -= velocidade;
        BottomPipe.TranslationX -= velocidade;

        if (BottomPipe.TranslationX < -larguraJanela)
        {
            BottomPipe.TranslationX = 13;
            TopPipe.TranslationX = 13;
        }
    }

    void AplicaPulo()
    {
        Bird.TranslationY -= forcaPulo;
        TempoPulando++;
        
        if (TempoPulando >= maxTempoPulando)
        {
            estaPulando = false;
            TempoPulando = 0;
        }
    }

    void Ui(object s, TappedEventArgs e)
    {
       
            estaPulando = true;
        
    }
bool VerificaColisao()
{
	if(!estaMorto)
	{
		if(VerificaColisaoTeto() || VerificaColisaoChao())
		{
			return true;
		}
	}
	return false;
}

	bool VerificaColisaoTeto(){
		var minY =- alturaJanela/2;
		if(Bird.TranslationY <= minY)
		return true;
		else
		return false;
	}
	bool VerificaColisaoChao(){
	var maxY = alturaJanela / 2 = chao.HeightRequest;
		if(Bird.TranslationY >= maxY)
		return true;
		else
		return false;
	}
	async Task desenha(){
		while(!estaMorto){
			AplicaGravidade();
			GerenciarCanos();
			if(VerificaColisao())
			{
				estaMorto = true;
				GameOverGrid.IsVisible = true;
				break;
			}
			await Task.Delay(tempoentreframes);
		}
	}
}





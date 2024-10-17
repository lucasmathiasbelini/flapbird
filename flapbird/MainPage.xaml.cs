namespace flapbird;

public partial class MainPage : ContentPage
{
    int score = 0;

    double larguraJanela = 0;
    double alturaJanela = 0;
    int velocidade = 20;

    const int gravidade = 1;
    const int tempoentreframes = 25;
    const int forcaPulo = 30;
    const int maxTempoPulando = 3;

    bool estaPulando = false;
    bool estaMorto = true;
    int TempoPulando = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    void AplicaGravidade()
    {
        Bird.TranslationY += gravidade;
    }

    async Task Desenha()
    {
        while (!estaMorto)
        {
            GerenciarCanos();
            if(estaPulando)
            {
                AplicaPulo();
            }
            else
            {
                AplicaGravidade();
            }
            if (VerificaColisao())
			{
				estaMorto = true;
				GameOverGrid.IsVisible = true;
				break;
			}
            await Task.Delay(tempoentreframes);
        }
    }

    void OnGameOverClicked(object s, TappedEventArgs a)
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
            BottomPipe.TranslationX = 12;
            TopPipe.TranslationX = 0;
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

    void OnGridCliked(object s, TappedEventArgs args)
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
		var minY =- alturaJanela / 2;
		if(Bird.TranslationY <= minY)
		    return true;
		else
		    return false;
	}

	bool VerificaColisaoChao(){
	var maxY = alturaJanela / 2 - chao.HeightRequest;
		if(Bird.TranslationY >= maxY)
		    return true;
		else
		    return false;
	}
}





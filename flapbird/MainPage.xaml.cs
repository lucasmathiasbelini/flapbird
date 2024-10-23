namespace flapbird;

public partial class MainPage : ContentPage
{
    int score = 0;
    double larguraJanela = 0;
    double alturaJanela = 0;
    const int velocidade = 20;
    const int gravidade = 1;
    const int tempoentreframes = 25;
    const int forcaPulo = 30;
    const int maxTempoPulando = 3;
    const int AberturaMinima = 20;
    bool estaPulando = false;
    bool estaMorto = false;
    int TempoPulando = 0;
    int pontuacao = 0;



    public MainPage()
    {
        InitializeComponent();
    }

    void AplicaGravidade()
    {
        Bird.TranslationY += gravidade;
    }

    async Task  Desenha()
    {
        while (!estaMorto)
        {
            AplicaGravidade();
            GerenciarCanos();
            if (estaPulando)
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

    void GerenciaCanos()
    {
        TopPipe.TranslationX -= velocidade;
        BottomPipe.TranslationX -= velocidade;

        if (BottomPipe.TranslationX < -larguraJanela)
        {
            BottomPipe.TranslationX = 12;
            TopPipe.TranslationX = 0;
            var AlturaMax = -100;
			var AlturaMin = - BottomPipe.HeightRequest;

			TopPipe.TranslationY = Random.Shared.Next((int)AlturaMin, (int)AlturaMax);
			BottomPipe.TranslationY = TopPipe.TranslationY + AberturaMinima + BottomPipe.HeightRequest;

			pontuacao++;
			AcabouScore.Text = "ACABO FI"
			 +
			 "     "
			 +
			  "Passou por :   " + pontuacao.ToString("D3") + "   Canos (Tu é TCHOLA)."; 
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
        if (!estaMorto)
        {
            if (VerificaColisaoTeto() || VerificaColisaoChao() || VerificaColisaoCanoCima())
            {
                return true;
            }
        }
        return false;
    }

    bool VerificaColisaoTeto()
    {
        var minY =- alturaJanela / 2;
        if (Bird.TranslationY <= minY)
            return true;
        else
            return false;
    }

    bool VerificaColisaoChao()
    {
        var maxY = alturaJanela / 2 - Chao.HeightRequest;
        if (Bird.TranslationY >= maxY)
            return true;
        else
            return false;
    }
    bool VerificaColisaoCanoCima()
    {
        var posHpassaro = (larguraJanela / 2) - (Bird.WidthRequest / 2);
        var posVpassaro = (larguraJanela / 2) - (Bird.HeightRequest / 2) + Bird.TranslationY;
        if (posHpassaro >= Math.Abs(TopPipe.TranslationX) - Bird.WidthRequest && 
        posHpassaro <= Math.Abs(Bird.TranslationX) + TopPipe.WidthRequest && 
        posHpassaro <= TopPipe.HeightRequest + Bird.TranslationY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    }






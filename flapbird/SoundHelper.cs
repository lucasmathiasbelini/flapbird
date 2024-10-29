using Plugin.Maui.Audio;
public class SoundHelper
{
  //------------------------------------------------------------------------

  public static void Play(string nomeArquivoWav)
  {
    Task.Run(async () =>
    {
    var audioFX = await FileSystem.OpenAppPackageFileAsync("motorola.mp4");
      var audioPlayer = AudioManager.Current.CreatePlayer(audioFX);
      audioPlayer.Play();
  });
  }

  //------------------------------------------------------------------------

}
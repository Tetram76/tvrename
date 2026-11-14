namespace TVRename;

public class MovieImage : MediaImage
{
    public int MovieId;
    public TVDoc.ProviderType MovieSource;

    public MovieImage()
    {
        Subject = ImageSubject.movie;
    }

    public MovieImage(int movieId, TVDoc.ProviderType source, XElement r) : base(r)
    {
        MovieId = r.ExtractInt("MovieId") ?? movieId; // thetvdb cachedSeries id
        MovieSource = source;
        Subject = ImageSubject.movie;
    }

    public XElement GenerateXElement()
    {
        return new XElement("MovieImage",
            GenerateCoreXElements(),
            new XElement("MovieId", MovieId)
        );
    }
}

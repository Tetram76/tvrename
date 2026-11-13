# Privacy Policy

This policy describes what the TV Rename application, as distributed from this repository, does with your data. TV Rename runs on your computer: it has no account, no server of its own, shows no advertising and collects no usage statistics.

## Data kept on your computer

TV Rename keeps on your computer, in its data folder (`%APPDATA%\TVRename`; the `/userfilepath:` command-line option places the settings and caches elsewhere):

- your settings and the description of your library (shows, movies, folders);
- a cache of the information and images downloaded from the online data sources;
- its log, which records its activity and errors and may contain the paths of your files and the names of your shows and movies;
- the cache of its built-in browser, including the cookies of the websites it displays.

None of this is sent anywhere by TV Rename. If you report a problem, you choose what to share, such as your log.

## Services TV Rename contacts

Like any internet request, each request reveals your IP address to the service that receives it. The services below receive only what each request needs; their own privacy policies apply to what they do with it.

Used by TV Rename in normal operation:

- **[TheTVDB](https://thetvdb.com), [TMDB](https://www.themoviedb.org) and [TVmaze](https://www.tvmaze.com)**, the online data sources: they receive the names you search for and the identifiers of the shows and movies of your library, with the API key of the application (not a key of yours), and serve the information and images TV Rename displays.
- **GitHub**: at start-up, TV Rename checks for a new version by reading the releases of the original TV Rename project on GitHub; when it finds one, it has GitHub convert its release notes for display. This check can be disabled in *Preferences → App Updates*.
- **pool.ntp.org**: at start-up, TV Rename asks a public time server for the current time, to correct your computer's clock in its own time calculations, such as air dates (not in offline mode). The request carries no data about you.
- **The websites displayed by the built-in browser** (pages of shows and movies on the data sources): they receive what any browser sends them, and may set cookies, kept in the browser cache described above.

Only if you configure or use the corresponding feature:

- **RSS feeds, JSON search and Jackett**, at the addresses you configure, to look for downloads of missing episodes: they receive show names, episode numbers or IMDb identifiers;
- **torrent and NZB clients** (qBittorrent, uTorrent, SABnzbd), at the addresses you configure, usually on your own computer or network;
- **YTS movie preview and recommendations** (*Beta* menu), which query a public movie catalogue;
- **links you open** from TV Rename (websites, search engines), which open in your own web browser.

## Changes to this policy

This policy is updated with the application, whenever what it describes changes. Its history is that of this file in the repository.

## Contact

For any question about this policy, open an [issue](https://github.com/Tetram76/tvrename/issues) in this repository.

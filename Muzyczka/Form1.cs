using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MojeD�wi�kiApp
{
    public partial class Form1 : Form
    {
        // Klasa lokalna do przechowywania danych o albumie
        public class Album
        {
            public string Artist { get; set; } = "";
            public string Title { get; set; } = "";
            public int SongsNumber { get; set; }
            public int Year { get; set; }
            public int DownloadNumber { get; set; }
        }

        // Lista album�w i indeks aktualnie wy�wietlanego
        List<Album> albums = new List<Album>();
        int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
            LoadAlbums();
            ShowAlbum();
        }

        private void LoadAlbums()
        {
            try
            {
                string[] allLines = File.ReadAllLines("Data.txt");
                List<string> lines = new List<string>();

                // Pomijamy puste linie
                foreach (string line in allLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        lines.Add(line.Trim());
                }

                for (int i = 0; i < lines.Count; i += 5)
                {
                    Album album = new Album
                    {
                        Artist = lines[i],
                        Title = lines[i + 1].Trim('"'),
                        SongsNumber = int.Parse(lines[i + 2]),
                        Year = int.Parse(lines[i + 3]),
                        DownloadNumber = int.Parse(lines[i + 4])
                    };
                    albums.Add(album);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("B��d wczytywania pliku: " + ex.Message);
            }
        }

        private void ShowAlbum()
        {
            if (albums.Count == 0) return;

            var album = albums[currentIndex];
            lblWykonawca.Text = album.Artist;
            lblTytul.Text = "\"" + album.Title + "\"";
            lblSzczegoly.Text = album.SongsNumber + " utwor�w";
            lblPobrania.Text = album.DownloadNumber.ToString();
        }

        // Obs�uga przycisku <<
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + albums.Count) % albums.Count;
            ShowAlbum();
        }

        // Obs�uga przycisku >>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % albums.Count;
            ShowAlbum();
        }

        // Obs�uga przycisku �Pobierz�
        private void button1_Click(object sender, EventArgs e)
        {
            albums[currentIndex].DownloadNumber++;
            ShowAlbum();
        }
    }
}
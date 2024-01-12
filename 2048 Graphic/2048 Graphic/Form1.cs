using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace _2048_Graphic
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        static Random rndgrid = new Random();
        static Random rnd = new Random();
        const int size = 4;
        int[,] grid = new int[size, size];
        int[,] tempgrid = new int[size, size];
        static bool move;
        int val = 0;

        public Form1()
        {
            InitializeComponent();

            RoundCorners(panelJeu, 50);
            RoundCorners(case1, 10);
            RoundCorners(case2, 10);
            RoundCorners(case3, 10);
            RoundCorners(case4, 10);
            RoundCorners(case5, 10);
            RoundCorners(case6, 10);
            RoundCorners(case7, 10);
            RoundCorners(case8, 10);
            RoundCorners(case9, 10);
            RoundCorners(case10, 10);
            RoundCorners(case11, 10);
            RoundCorners(case12, 10);
            RoundCorners(case13, 10);
            RoundCorners(case14, 10);
            RoundCorners(case15, 10);
            RoundCorners(case16, 10);

            //Initialisation du tableau de base
            for (int i = 0; i < 2; i++)
            {
                addNumber();
            }
            RefreshScreen();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialisation de la partie graphique du pannel
        }

        /// <summary>
        /// Fonction pour modifier la partie graphique et arrondire les angles des panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="radius">Radius en DEG pour définir l'arondi choisis</param>
        private void RoundCorners(Panel panel, int radius)
        {
            // Créer une région avec des coins arrondis
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90); // Coin supérieur gauche
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90); // Coin supérieur droit
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90); // Coin inférieur droit
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90); // Coin inférieur gauche
            path.CloseFigure();

            // Appliquer la région au panel
            panel.Region = new Region(path);
        }

        /// <summary>
        /// Fonction de Refresh des label, gère la non visibilité des numéro 0, applique la couleur au refresh de l'écran et gère la taille de la police quand le chiffre est trop grand.
        /// </summary>
        private void RefreshScreen()
        {
            //Refresh des cases
            label1.Text = grid[0, 0].ToString();
            label2.Text = grid[0, 1].ToString();
            label3.Text = grid[0, 2].ToString();
            label4.Text = grid[0, 3].ToString();
            label5.Text = grid[1, 0].ToString();
            label6.Text = grid[1, 1].ToString();
            label7.Text = grid[1, 2].ToString();
            label8.Text = grid[1, 3].ToString();
            label9.Text = grid[2, 0].ToString();
            label10.Text = grid[2, 1].ToString();
            label11.Text = grid[2, 2].ToString();
            label12.Text = grid[2, 3].ToString();
            label13.Text = grid[3, 0].ToString();
            label14.Text = grid[3, 1].ToString();
            label15.Text = grid[3, 2].ToString();
            label16.Text = grid[3, 3].ToString();

            //Affiche la case uniquement si le nombre n'est pas 0
            Label[] labelList = { label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            foreach (Label label in labelList)
            {
                if (label.Text == "0")
                {
                    label.Visible = false;
                }
                else
                {
                    label.Visible = true;
                }
            }

            //Redimention la taille de la police si le numéro est trop grand
            foreach (Label label in labelList)
            {
                int val = Int32.Parse(label.Text);

                if (val > 8192)
                {
                    if (label.Font.Size >= 5)
                    {
                        label.Font = new System.Drawing.Font(label.Font.FontFamily, label.Font.Size - 5);
                    }

                }


            }

            //Affiche la couleur
            foreach (Label label in labelList)
            {
                LabelColor(label);
            }
        }

        /// <summary>
        /// Fonction d'affichage des couleures
        /// </summary>
        /// <param name="label">label correspondant pour changer le backColor</param>
        private void LabelColor(Label label)
        {
            switch (label.Text)
            {
                case "2":
                    label.BackColor = Color.Red;
                    break;
                case "4":
                    label.BackColor = Color.Blue;
                    break;
                case "8":
                    label.BackColor = Color.Green;
                    break;
                case "16":
                    label.BackColor = Color.Yellow;
                    break;
                case "32":
                    label.BackColor = Color.Magenta;
                    break;
                case "64":
                    label.BackColor = Color.Cyan;
                    break;
                case "128":
                    label.BackColor = Color.DarkCyan;
                    break;
                case "256":
                    label.BackColor = Color.Gray;
                    break;
                case "512":
                    label.BackColor = Color.IndianRed;
                    break;
                case "1024":
                    label.BackColor = Color.DarkBlue;
                    break;
                case "2048":
                    label.BackColor = Color.DarkGreen;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        /// <summary>
        /// Fonction d'ajout des nombres dans les cases.
        /// </summary>
        private void addNumber()
        {
            int nbrow = rndgrid.Next(0, 4);
            int nbcol = rndgrid.Next(0, 4);

            int pourcent = rnd.Next(0, 100);

            if (grid[nbrow, nbcol] == 0)
            {
                if (pourcent >= 90)
                {
                    grid[nbrow, nbcol] = 4;
                }
                else
                {
                    grid[nbrow, nbcol] = 2;
                }
            }
            else addNumber();
        }

        /// <summary>
        /// Fonction de vérification de la touche pressée
        /// </summary>
        /// <param name="key"></param>
        void moved(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    moveUp();
                    break;

                case Keys.Down:
                    moveDown();
                    break;

                case Keys.Left:
                    moveLeft();
                    break;

                case Keys.Right:
                    moveRight();
                    break;

                default:
                    break;

            }
        }

        //Déplacement lors de la touche UP
        void moveUp()
        {
            // Utilisation d'un tableau auxiliaire pour marquer les tuiles déjà fusionnées
            bool[,] merged = new bool[size, size];

            for (int j = 0; j < size; j++)
            {
                for (int i = 1; i < size; i++)
                {
                    if (grid[i, j] != 0)
                    {
                        int currentTile = grid[i, j];
                        int k;

                        // Trouver la position où déplacer ou fusionner la tuile actuelle
                        for (k = i - 1; k >= 0 && grid[k, j] == 0; k--)
                        {
                            // Déplacement vers le haut tant que la case est vide
                            grid[k, j] = currentTile;
                            grid[k + 1, j] = 0;
                            move = true;
                        }

                        // Fusionner avec une tuile de même valeur si possible
                        if (k >= 0 && grid[k, j] == currentTile && !merged[k, j])
                        {
                            grid[k, j] *= 2;
                            int val = Int32.Parse(score.Text); // Assurez-vous que score est accessible depuis cette fonction
                            val += grid[k, j];
                            score.Text = val.ToString();
                            grid[i, j] = 0;
                            move = true;
                            merged[k, j] = true;
                        }
                    }
                }

                // Réinitialiser le tableau auxiliaire pour la prochaine colonne
                for (int k = 0; k < size; k++)
                {
                    merged[k, j] = false;
                }
            }
        }

        //Déplacement lors de la touche DOWN
        void moveDown()
        {
            // Utilisation d'un tableau auxiliaire pour marquer les tuiles déjà fusionnées
            bool[,] merged = new bool[size, size];

            for (int j = 0; j < size; j++)
            {
                for (int i = size - 2; i >= 0; i--)
                {
                    if (grid[i, j] != 0)
                    {
                        int currentTile = grid[i, j];
                        int k;

                        // Trouver la position où déplacer ou fusionner la tuile actuelle
                        for (k = i + 1; k < size && grid[k, j] == 0; k++)
                        {
                            // Déplacement vers le bas tant que la case est vide
                            grid[k, j] = currentTile;
                            grid[k - 1, j] = 0;
                            move = true;
                        }

                        // Fusionner avec une tuile de même valeur si possible
                        if (k < size && grid[k, j] == currentTile && !merged[k, j])
                        {
                            grid[k, j] *= 2;
                            int val = Int32.Parse(score.Text); // Assurez-vous que score est accessible depuis cette fonction
                            val += grid[k, j];
                            score.Text = val.ToString();
                            grid[i, j] = 0;
                            move = true;
                            merged[k, j] = true;
                        }
                    }
                }

                // Réinitialiser le tableau auxiliaire pour la prochaine colonne
                for (int k = 0; k < size; k++)
                {
                    merged[k, j] = false;
                }
            }
        }

        //Déplacement lors de la touche LEFT
        void moveLeft()
        {
            // Utilisation d'un tableau auxiliaire pour marquer les tuiles déjà fusionnées
            bool[,] merged = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 1; j < size; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        int currentTile = grid[i, j];
                        int k;

                        // Trouver la position où déplacer ou fusionner la tuile actuelle
                        for (k = j - 1; k >= 0 && grid[i, k] == 0; k--)
                        {
                            // Déplacement vers la gauche tant que la case est vide
                            grid[i, k] = currentTile;
                            grid[i, k + 1] = 0;
                            move = true;
                        }

                        // Fusionner avec une tuile de même valeur si possible
                        if (k >= 0 && grid[i, k] == currentTile && !merged[i, k])
                        {
                            grid[i, k] *= 2;
                            int val = Int32.Parse(score.Text); // Assurez-vous que score est accessible depuis cette fonction
                            val += grid[i, k];
                            score.Text = val.ToString();
                            grid[i, j] = 0;
                            move = true;
                            merged[i, k] = true;
                        }
                    }
                }

                // Réinitialiser le tableau auxiliaire pour la prochaine ligne
                for (int k = 0; k < size; k++)
                {
                    merged[i, k] = false;
                }
            }
        }

        //Déplacement lors de la touche RIGHT
        void moveRight()
        {
            // Utilisation d'un tableau auxiliaire pour marquer les tuiles déjà fusionnées
            bool[,] merged = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = size - 2; j >= 0; j--)
                {
                    if (grid[i, j] != 0)
                    {
                        int currentTile = grid[i, j];
                        int k;

                        // Trouver la position où déplacer ou fusionner la tuile actuelle
                        for (k = j + 1; k < size && grid[i, k] == 0; k++)
                        {
                            // Déplacement vers la droite tant que la case est vide
                            grid[i, k] = currentTile;
                            grid[i, k - 1] = 0;
                            move = true;
                        }

                        // Fusionner avec une tuile de même valeur si possible
                        if (k < size && grid[i, k] == currentTile && !merged[i, k])
                        {
                            grid[i, k] *= 2;
                            int val = Int32.Parse(score.Text); // Assurez-vous que score est accessible depuis cette fonction
                            val += grid[i, k];
                            score.Text = val.ToString();
                            grid[i, j] = 0;
                            move = true;
                            merged[i, k] = true;
                        }
                    }
                }



                // Réinitialiser le tableau auxiliaire pour la prochaine ligne
                for (int k = 0; k < size; k++)
                {
                    merged[i, k] = false;
                }
            }
        }

        private bool isFailed()
        {
            //Vérifie si il reste des emplacements vide dans la grille
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            if (move)
            {
                return false;
            }
            return true;
        }

        //Fonction de victoire
        bool isWin()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] >= 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Evénement de touche pressée
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(isFailed());
            if (!isFailed())
            {
                if (isWin()) { WinText.Visible = true; } //Affiche le message de victoire

                move = false;
                Array.Copy(grid, tempgrid, grid.Length); //Copie de l'array pour vérifier si un changement c'est passé

                // Obtenir la touche pressée
                Keys key = e.KeyCode;

                // Appeler la méthode "moved" avec la touche pressée
                moved(key);

                //Vérification d'un changement entre le tableau précédent et le tableau modifié
                bool sontEgaux = true;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] != tempgrid[i, j])
                        {
                            sontEgaux = false;
                            break;
                        }
                    }
                }

                if (move && !sontEgaux) addNumber(); //Si un déplacement à été effectué et que les tableaux ne sont pas égaux
                Array.Clear(tempgrid);
                RefreshScreen();
            }
            else
            {
                DialogResult result = MessageBox.Show("Vous avez perdu !!!\nVous souhaitez recommencer ou quitter ?", "PERDU", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //Réinitialisation de la partie
                    score.Text = "0";

                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            grid[i, j] = 0;
                        }
                    }

                    //Initialisation du tableau de base
                    for (int i = 0; i < 2; i++)
                    {
                        addNumber();
                    }
                    RefreshScreen();
                    WinText.Visible = false;
                }
                if (result == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }

        }


        //-----------------------------------------------------------//
        //                      ToolStripMenu                        //
        //-----------------------------------------------------------//

        //Menu Quiter : Permer de quitter l'application
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //Menu de nouvelle partie : Permet de recommencer la partie à 0 avec un une messageBox pour valider le choix
        private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Etes vous sur de recommencer une nouvelle partie ?", "Nouvelle partie", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //Réinitialisation de la partie
                score.Text = "0";

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        grid[i, j] = 0;
                    }
                }

                //Initialisation du tableau de base
                for (int i = 0; i < 2; i++)
                {
                    addNumber();
                }
                RefreshScreen();
            }
        }

        //Menu de sauvegarde : Permet de sauvegarder l'état de sa partie dans un fichier .txt
        private void sauvegarderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Sauvegarde des données de la partie dans un fichier texte
            string exportGrid = "";
            int k = 0;
            exportGrid += "Tableau : ";
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    exportGrid += grid[i, j].ToString() + " ";
                    k++;
                }
            }
            exportGrid += "\nScore : " + score.Text;

            //Ouverture de la boite de dialogue pour enregistrer le fichier
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("yyMMddhhmm" + "_2048_backup"); //Défini le nom par défaut du backup
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Fichiers texte (*.txt)|*.txt";
            saveFileDialog.Title = "Sélectionnez l'emplacement de la sauvegarde";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, exportGrid);
                MessageBox.Show("Votre fichier à bien été sauvegardé", "Sauvegarde réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Menu de chargement d'une sauvegarde : Permet de charger une sauvegarde précédente à parti d'un fichier .txt avec gestion de savegarde invalide
        private void chargerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt";
            openFileDialog.Title = "Sélectionnez un fichier de sauvegarde";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    // Assurez-vous que le fichier de sauvegarde a le format attendu
                    if (lines.Length >= 2 && lines[0].StartsWith("Tableau : ") && lines[1].StartsWith("Score : "))
                    {
                        string[] gridValues = lines[0].Substring("Tableau : ".Length).Split(' ');
                        int[,] importedGrid = new int[size, size];
                        int k = 0;

                        for (int i = 0; i < importedGrid.GetLength(0); i++)
                        {
                            for (int j = 0; j < importedGrid.GetLength(1); j++)
                            {
                                if (k < gridValues.Length)
                                {
                                    importedGrid[i, j] = int.Parse(gridValues[k]);
                                    k++;
                                }
                            }
                        }

                        // Mettez à jour votre grille avec les données importées
                        grid = importedGrid;

                        // Mettez à jour le score avec les données importées
                        string importedScore = lines[1].Substring("Score : ".Length);
                        score.Text = importedScore;

                        RefreshScreen();

                        MessageBox.Show("Fichier chargé avec succès.", "Chargement réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Le format du fichier de sauvegarde n'est pas valide.", "Erreur de chargement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement du fichier : " + ex.Message, "Erreur de chargement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
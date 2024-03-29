using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Castle.Core.Resource;
using CoinsCollection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Windows.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
namespace CoinsCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppContext _context = new AppContext();
        private CollectionViewSource materialsViewSource;
        private CollectionViewSource countriesViewSource;
        private CollectionViewSource albumsViewSource;
        
        public MainWindow()
        {
            InitializeComponent();
            materialsViewSource = (CollectionViewSource)FindResource(nameof(materialsViewSource));
            countriesViewSource = (CollectionViewSource)FindResource(nameof(countriesViewSource));
            albumsViewSource = (CollectionViewSource)FindResource(nameof(albumsViewSource));            
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {

            // load the entities into EF Core
            _context.Materials.Load();
            _context.Countries.Load();
            _context.Albums.Load();
            // bind to the source
            materialsViewSource.Source = _context.Materials.Local.ToObservableCollection();
            countriesViewSource.Source = _context.Countries.Local.ToObservableCollection();
            albumsViewSource.Source = _context.Albums.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _context.SaveChanges();
            // clean up database connections
            _context.Dispose();
            base.OnClosing(e);
        }

        private void RefreshDBs()
        {
            _context.SaveChanges();
            var albumsSorted = from o in _context.Albums
                                orderby o.Name ascending
                                select new { ID = o.ID, Name = o.Name };
            var albums = (from o in albumsSorted
                          select o.Name).ToList();
            albums.Insert(0, "");
            
            AlbumsComboBox.ItemsSource = albums;

            var materialsSorted = from o in _context.Materials
                               orderby o.Name ascending
                               select new { ID = o.ID, Name = o.Name };
            var materials = (from o in materialsSorted
                          select o.Name).ToList();
            materials.Insert(0, "");
            
            MaterialsComboBox.ItemsSource = materials;

            var countriesSorted = from o in _context.Countries
                               orderby o.Name ascending
                               select new { ID = o.ID, Name = o.Name };
            var countries = (from o in countriesSorted
                          select o.Name).ToList();
            countries.Insert(0, "");
            
            CountriesComboBox.ItemsSource = countries;
        }

        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region CountriesGrid

        private string nameCountryFilter = "";
        private int fromYearCountryFilter = YearConstants.MinimumYear;
        private int tillYearCountryFilter = YearConstants.MaximumYear;
        private string countrySortLastSelectedHeaderName = ""; 
        ListSortDirection countrySortDirection = ListSortDirection.Descending;
        private void CountriesViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            CoinCountry? t = e.Item as CoinCountry;
            if (t != null)
            {
                if (countriesFilterCheckbox.IsChecked == true)
                {
                    if ((!nameCountryFilter.IsNullOrEmpty() && t.Name.ToLowerInvariant().IndexOf(nameCountryFilter.ToLowerInvariant()) < 0) ||
                        (t.From > tillYearCountryFilter || t.Till < fromYearCountryFilter))
                        e.Accepted = false;
                }
            }
        }

        private void nameCountriesFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameCountryFilter = nameCountriesFilterTextBox.Text;
            CountriesFiltersRefresh();
        }

        private void fromCountriesFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(fromCountriesFilterTextBox.Text, out fromYearCountryFilter))
                fromYearCountryFilter = YearConstants.MinimumYear;
            CountriesFiltersRefresh();
        }

        private void tillCountriesFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(tillCountriesFilterTextBox.Text, out tillYearCountryFilter))
                fromYearCountryFilter = YearConstants.MaximumYear;
            CountriesFiltersRefresh();
        }

        private void countriesFilterCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            CountriesFiltersRefresh();
        }

        private void CountriesFiltersRefresh()
        {
            CollectionViewSource.GetDefaultView(countriesDataGrid.ItemsSource).Refresh();
        }
        void countriesData_GridViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader? headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null)
            {
                ICollectionView cvCountries = CollectionViewSource.GetDefaultView(countriesDataGrid.ItemsSource);
                cvCountries.SortDescriptions.Clear();
                var newDirection = countrySortDirection;
                var selectedHeaderName = headerClicked.Name;
                if (selectedHeaderName != countrySortLastSelectedHeaderName)
                    newDirection = newDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                if (headerClicked.Equals(nameCountriesDataGridColumnHeader))
                    cvCountries.SortDescriptions.Add(new SortDescription("Name", newDirection));
                else if (headerClicked.Equals(fromCountriesDataGridColumnHeader))
                    cvCountries.SortDescriptions.Add(new SortDescription("From", newDirection));
                else if (headerClicked.Equals(tillCountriesDataGridColumnHeader))
                    cvCountries.SortDescriptions.Add(new SortDescription("Till", newDirection));
                else if (headerClicked.Equals(coinsCountriesFromDataGridColumnHeader))
                    cvCountries.SortDescriptions.Add(new SortDescription("CoinsCount", newDirection));
                else
                {
                    newDirection = countrySortDirection;
                    selectedHeaderName = countrySortLastSelectedHeaderName;
                }
                countrySortDirection = newDirection;
                countrySortLastSelectedHeaderName = selectedHeaderName;
            }
        }
        #endregion

        #region MaterialsGrid
        private string nameSynonymsMaterialFilter = "";
        private string materialsSortLastSelectedHeaderName = "";
        ListSortDirection materialsSortDirection = ListSortDirection.Descending;
        private void MaterialsViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            CoinMaterial? t = e.Item as CoinMaterial;
            if (t != null)
            {
                if (materialsFilterCheckbox.IsChecked == true)
                {
                    if (!nameSynonymsMaterialFilter.IsNullOrEmpty() &&
                        t.Name.ToLowerInvariant().IndexOf(nameSynonymsMaterialFilter.ToLowerInvariant()) < 0 &&
                        (t.Synonyms == null || t.Synonyms.IsNullOrEmpty() || t.Synonyms.ToLowerInvariant().IndexOf(nameSynonymsMaterialFilter.ToLowerInvariant()) < 0))
                        e.Accepted = false;
                }
            }
        }

        private void nameSynonymsMaterialsFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameSynonymsMaterialFilter = nameSynonymsMaterialsFilterTextBox.Text;
            MaterialsFiltersRefresh();
        }

        private void materialsFilterCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            MaterialsFiltersRefresh();
        }

        private void MaterialsFiltersRefresh()
        {
            CollectionViewSource.GetDefaultView(materialsDataGrid.ItemsSource).Refresh();
        }

        void materialsData_GridViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader? headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked != null)
            {
                ICollectionView cvMaterials = CollectionViewSource.GetDefaultView(materialsDataGrid.ItemsSource);
                cvMaterials.SortDescriptions.Clear();
                var newDirection = materialsSortDirection;
                var selectedHeaderName = headerClicked.Name;
                if (selectedHeaderName != materialsSortLastSelectedHeaderName)
                    newDirection = newDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                if (headerClicked.Equals(nameCountriesDataGridColumnHeader))
                    cvMaterials.SortDescriptions.Add(new SortDescription("Name", newDirection));
                else if (headerClicked.Equals(coinsCountriesFromDataGridColumnHeader))
                    cvMaterials.SortDescriptions.Add(new SortDescription("CoinsCount", newDirection));
                else
                {
                    newDirection = countrySortDirection;
                    selectedHeaderName = countrySortLastSelectedHeaderName;
                }
                countrySortDirection = newDirection;
                countrySortLastSelectedHeaderName = selectedHeaderName;
            }
        }
        #endregion

        #region AlbumsGrid

        System.Drawing.Bitmap? coverBitmap = null;
        private void albumsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AlbumPageCellsImage.Source = null;
            AlbumCoverImage.Source = null;
            try
            {
                var album = (CoinAlbum?)albumsDataGrid.SelectedItem;
                if (album != null)
                {
                    IntPtr hBitmap = 0;
                    if (album.Cover != null)
                    {
                        using (var ms = new MemoryStream(album.Cover))
                        {
                            coverBitmap = new Bitmap(ms);
                        }

                        try
                        {
                            hBitmap = coverBitmap.GetHbitmap();
                            AlbumCoverImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        }
                        finally
                        {
                            DeleteObject(hBitmap);
                        }
                    }
                    if (album.ColumnsCount > 1 && album.RowsCount > 1)
                    {
                        Bitmap cellsBitmap = new Bitmap((int)AlbumPageCellsImage.Width, (int)AlbumPageCellsImage.Height);
                        Graphics g = Graphics.FromImage(cellsBitmap);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                        g.FillRectangle(new SolidBrush(System.Drawing.Color.White), new RectangleF(0.0f, 0.0f, (float)AlbumPageCellsImage.Width, (float)AlbumPageCellsImage.Height));
                        var pen = new System.Drawing.Pen(System.Drawing.Color.Black, 2);
                        for (int i = 0; i <= album.ColumnsCount; i++)
                        {
                            var width = (float)(AlbumPageCellsImage.Width / album.ColumnsCount * i);
                            g.DrawLine(pen, width, 0.0f, width, (float)AlbumPageCellsImage.Height);
                        }
                        for (int i = 0; i <= album.RowsCount; i++)
                        {
                            var height = (float)(AlbumPageCellsImage.Height / album.RowsCount * i);
                            g.DrawLine(pen, 0.0f, height, (float)AlbumPageCellsImage.Width, height);
                        }
                        try
                        {
                            hBitmap = cellsBitmap.GetHbitmap();
                            AlbumPageCellsImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        }
                        finally
                        {
                            DeleteObject(hBitmap);
                        }
                    }
                }
                else
                {
                    coverBitmap = null;
                    AlbumCoverImage.Source = null;
                }
            }
            catch (System.InvalidCastException)
            {
                coverBitmap = null;
                AlbumCoverImage.Source = null;
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void LoadAlbumCover_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Pictures"; // Default file name
            dialog.DefaultExt = ".bmp"; // Default file extension
            dialog.Filter = "BMP files|*.bmp"; // Filter files by extension

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                coverBitmap = (Bitmap)Bitmap.FromFile(filename);
                if (coverBitmap != null)
                {
                    IntPtr hBitmap = 0;
                    try
                    {
                        hBitmap = coverBitmap.GetHbitmap();
                        AlbumCoverImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                    finally
                    {
                        DeleteObject(hBitmap);
                    }
                }
            }
        }
        private void PastAlbumCover_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                if (clipboardData != null)
                {
                    if (clipboardData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                    {
                        coverBitmap = (System.Drawing.Bitmap)clipboardData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                        if (coverBitmap != null)
                        {
                            IntPtr hBitmap = 0;
                            try
                            {
                                hBitmap = coverBitmap.GetHbitmap();
                                AlbumCoverImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            }
                            finally
                            {
                                DeleteObject(hBitmap);
                            }
                        }
                    }
                }
            }
        }

        private void SaveAlbumCover_Click(object sender, RoutedEventArgs e)
        {
            var album = (CoinAlbum?)albumsDataGrid.SelectedItem;
            if (album != null && coverBitmap != null)
            {
                using (var mStream = new MemoryStream())
                {
                    coverBitmap.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    album.Cover = mStream.ToArray();
                }
            }
        }



        #endregion

        #region CoinsGrid

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDBs();
        }
        #endregion
    }
}
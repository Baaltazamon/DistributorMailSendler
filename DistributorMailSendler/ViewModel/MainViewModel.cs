using DistributorMailSendler.DistributorsDebtors;
using DistributorMailSendler.Model;
using DistributorMailSendler.Model.Database;
using DistributorMailSendler.View;
using DistributorMailSendler.View.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml;

namespace DistributorMailSendler.ViewModel
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        private string PathFile;
        private string _fileName;
        public string FileName { 
            get { return _fileName; } 
            set { _fileName = value; OnPropertyChanged(); }
        }
        
        private int _typeDocument;
        private bool _processing;

        public int TypeDocument { get { return _typeDocument; }
            set
            {
                _typeDocument = value;
                OnPropertyChanged();
            } 
        }
        private bool CanSavetoDB;
        private bool _processingLoadXml;
        private int _progressValue;

        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged();
            }
        }
        public bool Processing { get { return _processing; }
            set
            {
                _processing = value;
                OnPropertyChanged();
            }
        }
        public bool ProcessingLoadXml
        {
            get { return _processingLoadXml; }
            set
            {
                _processingLoadXml = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> TypeDocuments { get; private set; }
        public MainCommand OpenFileCommand { get; set; }
        public MainCommand SaveToDatabaseCommand { get; set; }
        public MainCommand GetPositionsList { get; set; }
        public MainCommand OpenInfo { get; set; }
        public MainCommand GetGroupPositions { get; set; }
        public ObservableCollection<Asp_SpravDistr> Debitors { get; set; }
        public ObservableCollection<Nomenclature> Nomenclatures { get; private set; }
        public ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition> Positions { get; set; }
        public ObservableCollection<Outlet> Outlets { get; private set; }
        public ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoice> Turnovers { get; private set; }
        public ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoice> Remains { get; private set; }
        private CRMEntities db { get; set; }
        public delegate void VisibleChanger();
        public event VisibleChanger VisChange;

        public MainViewModel()
        {
            CanSavetoDB = false;
            db = new CRMEntities();
            TypeDocuments = new ObservableCollection<string>
            {
                "Номенклатура",
                "Обороты",
                "Остатки",
                "Торговые точки"
            };
            TypeDocument = -1;
            Positions = new ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition>();
            Processing = false;
            OpenFileCommand = new MainCommand(OpenXml, CanOpenFile);
            SaveToDatabaseCommand = new MainCommand(SaveToDataBase, CanSave);
            GetPositionsList = new MainCommand(GetPositions);
            GetGroupPositions = new MainCommand(GetPositions, CanGetPositions);
            OpenInfo = new MainCommand(OpenView, CanOpenFile);
            FileName = "Файл не выбран";
            LoadDebitors();
        }

        private void FailLoadDock()
        {
            CanSavetoDB = false;
            MessageBox.Show("Проверьте выбор файла или его типа в выпадающем списке.", "Неверный файл или тип", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void GetPositions(object obj)
        {
            var remains = (DocumentDistributorInvoice.objectDocumentDistributorInvoice)obj;
            Positions = new ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition>(remains.Positions.ToList());
            
            
            
        }
        
        private void ProgressValueCulc(double Count, double Current)
        {
            ProgressValue = Convert.ToInt32(Math.Round(Current/Count * 100));
        }
        

        private bool CanGetPositions(object obj)
        {
            if (obj is null)
                return false;
            else
                return true;
        }
        private void OpenView(object obj)
        {
            DataViewWindow dataViewWindow = new DataViewWindow(new System.Windows.Controls.UserControl());
            switch (TypeDocument)
            {
                case 0:
                    dataViewWindow = new DataViewWindow(new NomenclatureControl(this)); break;
                case 1:
                    dataViewWindow = new DataViewWindow(new TurnoverControl(this)); break;
                case 2:
                    dataViewWindow = new DataViewWindow(new RemainsControl(this)); break;
                case 3:
                    dataViewWindow = new DataViewWindow(new OutletControl(this)); break;
                default:
                    break;
            }
            dataViewWindow.Show();
        }
        private void SaveToDataBase(object obj)
        {
            var distr = obj as Asp_SpravDistr;
            Processing = true;
            VisChange();
            CanSavetoDB = false;
            switch (TypeDocument)
            {
                case 0:
                    LoadNomenclatureToDatabase(distr);break;
                case 1:
                    LoadInvoiceToDatabase(distr, Turnovers); break;
                case 2:
                    LoadInvoiceToDatabase(distr, Remains); break;
                case 3: 
                    LoadOutletToDatebase(distr);break;
                default:
                    break;
            }

        }
        public async void LoadInvoiceToDatabase(Asp_SpravDistr distr, ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoice> invoice)
        {
            await Task.Run(() =>
            {
                var client = new DocumentDistributorInvoice.DocumentDistributorInvoiceClient();
                if (distr is null)
                {
                    MessageBox.Show("Выберите дистрибьютера", "Дистрибьютер не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                    Processing = false;
                    VisChange();
                    return;
                }
                client.ClientCredentials.UserName.UserName = distr.Login;
                client.ClientCredentials.UserName.Password = distr.Password;
                client.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 10, 0);
                client.Open();
                for (int i = 0; i < invoice.Count; i++)
                {
                    try
                    {

                        client.InsertDistributorInvoice(invoice[i]);
                        
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                    ProgressValueCulc(invoice.Count, i + 1);

                }
                client.Close();
                MessageBox.Show("Загрузка завершена", "Загрузка", MessageBoxButton.OK, MessageBoxImage.Information);
                Processing = false;
                VisChange();
            });
        }
        public async void LoadNomenclatureToDatabase(Asp_SpravDistr distr)
        {
            await Task.Run(() =>
            {
                var client = new OldDistributorsMaterialArticul.DistributorsMaterialArticulClient();
                if (distr is null)
                {
                    MessageBox.Show("Выберите дистрибьютера", "Дистрибьютер не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                    Processing = false;
                    VisChange();
                    return;
                }
                client.ClientCredentials.UserName.UserName = distr.Login;
                client.ClientCredentials.UserName.Password = distr.Password;
                client.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 10, 0);
                client.Open();
                for (int i = 0; i < Nomenclatures.Count; i++)
                {
                    try
                    {
                        client.CollateMaterArticul(Nomenclatures[i].CodeEAN, Nomenclatures[i].Articul, Nomenclatures[i].MaterName);
                        
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                    ProgressValueCulc(Nomenclatures.Count, i + 1);
                }
                client.Close();
                MessageBox.Show("Загрузка завершена", "Загрузка", MessageBoxButton.OK, MessageBoxImage.Information);
                Processing = false;
                VisChange();
            });
        }

        public async void LoadOutletToDatebase(Asp_SpravDistr distr)
        {
            await Task.Run(() =>
            {
                var client = new DistributorsDebtors.DistributorsDebtorsClient();
                if (distr is null)
                {
                    MessageBox.Show("Выберите дистрибьютера", "Дистрибьютер не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                    Processing = false;
                    VisChange();
                    return;
                }
                client.ClientCredentials.UserName.UserName = distr.Login;
                client.ClientCredentials.UserName.Password = distr.Password;
                client.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.SendTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                client.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 10, 0);
                
                client.Open();
                for (int i = 0; i < Outlets.Count; i++)
                {
                    try
                    {
                        client.CreateDebtorAndOutlet(Outlets[i].PartnerINN, Outlets[i].PartnerKPP, Outlets[i].PartnerName,
                            Outlets[i].PartnerAddress, Outlets[i].OutletName, Outlets[i].OutletKPP, Outlets[i].OutletAddress, Outlets[i].PartnerCode,
                            Outlets[i].OutletCode, Outlets[i].CodeDistr);
                        MessageBox.Show(Outlets[i].OutletName);


                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
                    ProgressValueCulc(Outlets.Count, i + 1);
                }

                client.Close();
                MessageBox.Show("Загрузка завершена", "Загрузка", MessageBoxButton.OK, MessageBoxImage.Information);
                Processing = false;
                VisChange();
            });
        }

        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private void LoadDebitors()
        {
            List<Asp_SpravDistr> distrs = db.Asp_SpravDistr.Where(c=> !c.ShortName.Contains("test")).OrderBy(c=> c.ShortName).ToList();
            Debitors = new ObservableCollection<Asp_SpravDistr>(distrs);

        }
        public async void OpenXml(object obj)
        {
            await Task.Run(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    PathFile = openFileDialog.FileName;
                    FileName = openFileDialog.SafeFileName;
                    ProcessingLoadXml = true;
                    LoadingFile(obj);

                }
            });
        }

        private async void LoadingFile(object obj)
        {
            await Task.Run(() =>
            {
                if (TypeDocument == -1)
                    MessageBox.Show("Выберите тип документа", "Тип документа не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (TypeDocument == 0)
                {
                    LoadNomenclatures();
                }
                else if (TypeDocument == 1)
                {
                    LoadTurnovers();
                }
                else if (TypeDocument == 2)
                {
                    LoadRemains();
                }
                else if (TypeDocument == 3)
                {
                    LoadOutlets();
                }
                if (!CanSavetoDB)
                {
                    CanSavetoDB = true;
                    
                }
                ProcessingLoadXml = false;
            });
            
        }

        private void LoadNomenclatures()
        {
            Nomenclatures = new ObservableCollection<Nomenclature>();
            XmlDocument document = new XmlDocument();
            document.Load(PathFile);
            XmlElement element = document.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                if (xnode.Name == "SKU")
                {
                    Nomenclature nomenclature = new Nomenclature();
                    foreach (XmlNode cnode in xnode.ChildNodes)
                    {

                        if (cnode.Name == "CodeDistr")
                            nomenclature.CodeDistr = Convert.ToInt64(cnode.FirstChild.Value);
                        else if (cnode.Name == "CodeEAN")
                            nomenclature.CodeEAN = Convert.ToInt64(cnode.FirstChild.Value);
                        else if (cnode.Name == "Articul")
                            nomenclature.Articul = cnode.FirstChild.Value;
                        else if (cnode.Name == "MaterName")
                            nomenclature.MaterName = cnode.FirstChild.Value;

                    }
                    if (nomenclature != null)
                        Nomenclatures.Add(nomenclature);
                }
                else
                {
                    FailLoadDock();
                    break;
                }
            }
        }

        private void LoadOutlets()
        {
            Outlets = new ObservableCollection<Outlet>();
            XmlDocument document = new XmlDocument();
            document.Load(PathFile);
            XmlElement element = document.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                if (xnode.Name == "Outlet")
                {
                    Outlet outlet = new Outlet();
                    foreach (XmlNode cNode in xnode.ChildNodes)
                    {

                        if (cNode.Name == "CodeDistr")
                            outlet.CodeDistr = Convert.ToDouble(cNode.FirstChild.Value);
                        else if (cNode.Name == "PartnerCode")
                            outlet.PartnerCode = cNode.FirstChild.Value;
                        else if (cNode.Name == "PartnerName")
                            outlet.PartnerName = cNode.FirstChild.Value;
                        else if (cNode.Name == "PartnerAddress")
                            outlet.PartnerAddress = cNode.FirstChild.Value;
                        else if (cNode.Name == "PartnerINN")
                            outlet.PartnerINN = cNode.FirstChild.Value;
                        else if (cNode.Name == "PartnerKPP")
                            outlet.PartnerKPP = cNode.FirstChild.Value;
                        else if (cNode.Name == "OutletCode")
                            outlet.OutletCode = cNode.FirstChild.Value;
                        else if (cNode.Name == "OutletKPP")
                            outlet.OutletKPP = cNode.FirstChild.Value;
                        else if (cNode.Name == "OutletName")
                            outlet.OutletName = cNode.FirstChild.Value;
                        else if (cNode.Name == "OutletAddress")
                            outlet.OutletAddress = cNode.FirstChild.Value;

                    }
                    Outlets.Add(outlet);
                }
                else
                {
                    FailLoadDock();
                    break;
                }
            }
        }

        private void LoadRemains()
        {
            Remains = new ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoice>();
            XmlDocument document = new XmlDocument();
            document.Load(PathFile);
            XmlElement element = document.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                if (xnode.Name == "objectDocumentDistributorInvoice")
                {
                    DocumentDistributorInvoice.objectDocumentDistributorInvoice remains = new DocumentDistributorInvoice.objectDocumentDistributorInvoice();
                    List<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition> posi = new List<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition>();
                    foreach (XmlNode cNode in xnode.ChildNodes)
                    {

                        if (cNode.Name == "OperDate")
                            remains.OperDate = Convert.ToDateTime(cNode.FirstChild.Value);
                        else if (cNode.Name == "PartnerFromCodeDistr")
                            remains.PartnerFromCodeDistr = cNode.FirstChild.Value;
                        else if (cNode.Name == "TypeOper")
                            remains.TypeOper = Convert.ToInt32(cNode.FirstChild.Value);
                        else if (cNode.Name == "Positions")
                        {
                            foreach (XmlNode position in cNode.ChildNodes)
                            {
                                if (position.Name == "objectDocumentDistributorInvoicePosition")
                                {
                                    DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition pos = new DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition();
                                    foreach (XmlNode cPosition in position.ChildNodes)
                                    {
                                        if (cPosition == null)
                                            continue;
                                        if (cPosition.Name == "Articul")
                                            pos.Articul = cPosition.FirstChild.Value;
                                        else if (cPosition.Name == "Price")
                                            pos.Price = double.Parse(cPosition.FirstChild.Value.Replace('.', ','));
                                        else if (cPosition.Name == "Quantity")
                                            pos.Quantity = Convert.ToInt32(cPosition.FirstChild.Value);

                                    }
                                    posi.Add(pos);
                                }
                                else
                                {
                                    FailLoadDock();
                                    break;
                                }
                            }
                            remains.Positions = posi.ToArray();
                        }

                    }
                    Remains.Add(remains);
                    Positions = new ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition>(remains.Positions.ToList());
                }
                else
                {
                    FailLoadDock();
                    break;
                }
            }
        }

        private void LoadTurnovers()
        {
            Turnovers = new ObservableCollection<DocumentDistributorInvoice.objectDocumentDistributorInvoice>();
            XmlDocument document = new XmlDocument();
            document.Load(PathFile);
            XmlElement element = document.DocumentElement;
            foreach (XmlNode xnode in element)
            {
                if (xnode.Name == "objectDocumentDistributorInvoice")
                {
                    DocumentDistributorInvoice.objectDocumentDistributorInvoice turnover = new DocumentDistributorInvoice.objectDocumentDistributorInvoice();
                    List<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition> posi = new List<DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition>();
                    foreach (XmlNode cNode in xnode.ChildNodes)
                    {

                        if (cNode.Name == "OperDate")
                            turnover.OperDate = Convert.ToDateTime(cNode.FirstChild.Value);
                        else if (cNode.Name == "PartnerFromCodeDistr")
                        {
                            if (cNode.FirstChild == null)
                                continue;
                            turnover.PartnerFromCodeDistr = cNode.FirstChild.Value;
                        }
                        else if (cNode.Name == "TypeOper")
                            turnover.TypeOper = Convert.ToInt32(cNode.FirstChild.Value);
                        else if (cNode.Name == "DocNumberDistr")
                            turnover.DocNumberDistr = cNode.FirstChild.Value;
                        else if (cNode.Name == "AgentCodeDistr")
                            turnover.AgentCodeDistr = (cNode.FirstChild == null) ? "" : cNode.FirstChild.Value;
                        else if (cNode.Name == "GUID")
                            turnover.GUID = cNode.FirstChild.Value;
                        else if (cNode.Name == "Notes")
                            turnover.Notes = (cNode.FirstChild == null) ? "" : cNode.FirstChild.Value;
                        else if (cNode.Name == "OutletDistr")
                            turnover.OutletDistr = (cNode.FirstChild == null) ? "" : cNode.FirstChild.Value;
                        else if (cNode.Name == "PartnerToCodeDistr")
                            turnover.PartnerToCodeDistr = (cNode.FirstChild == null) ? "" : cNode.FirstChild.Value;
                        else if (cNode.Name == "Positions")
                        {
                            foreach (XmlNode position in cNode.ChildNodes)
                            {
                                if (position.Name == "objectDocumentDistributorInvoicePosition")
                                {
                                    DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition pos = new DocumentDistributorInvoice.objectDocumentDistributorInvoicePosition();
                                    foreach (XmlNode cPosition in position.ChildNodes)
                                    {

                                        if (cPosition.Name == "Articul")
                                            pos.Articul = cPosition.FirstChild.Value;
                                        else if (cPosition.Name == "Price")
                                        {
                                            pos.Price = Convert.ToDouble(cPosition.FirstChild.Value.Replace('.', ','));
                                        }

                                        else if (cPosition.Name == "Quantity")
                                            pos.Quantity = Convert.ToInt32(cPosition.FirstChild.Value);

                                    }
                                    posi.Add(pos);
                                }
                                else
                                {
                                    FailLoadDock();
                                    break;
                                }
                            }
                            turnover.Positions = posi.ToArray();
                        }

                    }
                    Turnovers.Add(turnover);
                }
                else
                {
                    FailLoadDock();
                    break;
                }
            }
        }
        private bool CanOpenFile(object typeDoc)
        {
            if (TypeDocument == -1)
                return false;
            else
                return true;
        }
        private bool CanSave(object typeDoc)
        {
            if (CanSavetoDB)
                return true;
            else
                return false;
        }
    }
}

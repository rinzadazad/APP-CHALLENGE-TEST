using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using System.Threading;
using Android.Graphics;
using Android.Util;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TESTAPP
{
    [Activity(Label = "act_Transaction_Detail", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class act_Transaction_Detail : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        EditText txt_TransRefNo_obj;
        EditText txtceno;
        EditText txtBeneficiaryName;
        EditText txtbenbank;
        EditText txtpayoutlocation;
        EditText txtAccNo;
        EditText txtPaymentdate;
        TextView lbl_totpaid;
        TextView lbl_recamt;

        List<cls_Transaction_Detail> Transaction_Detail_list = new List<cls_Transaction_Detail>();
        ListView list_view_Package;
        Button btnPkgContinue;
        string PromoCode;
        bool bCouponItemSelected = false;
        bool bCoolerItemSelected = false;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.lyt_Transaction_Details);
            PromoCode = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("PromoCode"));
           

            txt_TransRefNo_obj = FindViewById<EditText>(Resource.Id.txtTransRefNo);
            txtceno = FindViewById<EditText>(Resource.Id.txtceno);
            txtBeneficiaryName = FindViewById<EditText>(Resource.Id.txtBeneficiaryName);
            txtbenbank = FindViewById<EditText>(Resource.Id.txtbenbank);
            txtpayoutlocation = FindViewById<EditText>(Resource.Id.txtpayoutlocation);
            txtAccNo = FindViewById<EditText>(Resource.Id.txtAccNo);
            txtPaymentdate = FindViewById<EditText>(Resource.Id.txtPaymentdate);
            lbl_totpaid = FindViewById<TextView>(Resource.Id.lbl_recamt);
            lbl_recamt = FindViewById<TextView>(Resource.Id.lbl_totpaid); 
            InitNavMenu();
            InitListMenu();
           
        }
      
        void InitNavMenu()
        {
            // Init toolbar
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.header_Package_Detail);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }
        async void InitListMenu()
        {
            var client = new HttpClient();
            var szResult = await client.GetAsync(new System.Uri("https://61769aed03178d00173dad89.mockapi.io/api/v1/transactions/"+ PromoCode));

            if (szResult.IsSuccessStatusCode)
            {
                var content = await szResult.Content.ReadAsStringAsync();
                Transaction_Detail_list = JsonConvert.DeserializeObject<List<cls_Transaction_Detail>>("["+content+"]");
            }
            txt_TransRefNo_obj.Text = Transaction_Detail_list[0].reference_number;
            txtceno.Text = Transaction_Detail_list[0].cf_number;
            txtBeneficiaryName.Text = Transaction_Detail_list[0].name;
            txtbenbank.Text = Transaction_Detail_list[0].bank_name;
            txtpayoutlocation.Text = Transaction_Detail_list[0].payout_location;
            txtAccNo.Text = Transaction_Detail_list[0].account_number;
            txtPaymentdate.Text = Transaction_Detail_list[0].createdAt;
            lbl_totpaid.Text = Transaction_Detail_list[0].paid_amount.ToString();
            lbl_recamt.Text = Transaction_Detail_list[0].receiving_amount.ToString();


            //btnPkgContinue = FindViewById<Button>(Resource.Id.btnPckContinue);
            //btnPkgContinue.Click += btnPkgContinue_Click;
        }
        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            //cls_statics.NavigationView_Main_NavigationItemSelected(sender, e, this, drawerLayout);
        }
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           

        }
    
        private void btnPkgContinue_Click(object sender, EventArgs e)
        {
          
        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(act_Transaction));
            this.StartActivity(intent);
            this.Finish();

        }
    }
}
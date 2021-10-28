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
using Android.Support.V4.View;

namespace TESTAPP
{
    [Activity(Label = "act_Transaction_Detail", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class act_Transaction : AppCompatActivity
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

        TabMissingAssetAdapter adapterMissingAsset;
        TabLayout tabLayoutMissingAssetObj;
        ViewPager viewPagerMissingAssetObj;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.lyt_Transaction);
            
            viewPagerMissingAssetObj = FindViewById<ViewPager>(Resource.Id.viewPagerMissingAsset);
            tabLayoutMissingAssetObj = FindViewById<TabLayout>(Resource.Id.tabLayoutMissingAsset);

            InitNavMenu();
            

            adapterMissingAsset = new TabMissingAssetAdapter(SupportFragmentManager);

            adapterMissingAsset.addFragment(new act_Transaction_Header(), "Remittance");
            adapterMissingAsset.addFragment(new act_Transaction_Header(), "Credit Card Payment");
            adapterMissingAsset.addFragment(new act_Transaction_Header(), "Travel Card Reload");
            adapterMissingAsset.addFragment(new act_Transaction_Header(), "Bill Payments");

            viewPagerMissingAssetObj.Adapter = (adapterMissingAsset);
            tabLayoutMissingAssetObj.SetupWithViewPager(viewPagerMissingAssetObj);
        }
      
        void InitNavMenu()
        {
            // Init toolbar
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.header_Package);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
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
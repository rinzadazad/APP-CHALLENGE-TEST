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
    //[Activity(Label = "act_Transaction_Header", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class act_Transaction_Header : Android.Support.V4.App.Fragment
    {
        DrawerLayout drawerLayout;
        List<cls_Transaction_Header> Transaction_list = new List<cls_Transaction_Header>();
        ListView list_view_Package;
        Button btnInvPrintObj;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            


        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = null;
            try
            {
                view = inflater.Inflate(Resource.Layout.lyt_Transaction_Header, container, false);
                InitListMenu(view);
            }
            catch
            { }
            return view;
        }
       
        async void InitListMenu(View View)
        {
            var progressDialog = ProgressDialog.Show(Activity, "Please wait...", "Fetching Data", true); new Thread(new ThreadStart(async delegate
            { })).Start();
            //Transaction_list = cls_database.GetAllPackages();
            var client = new HttpClient();
            var szResult = await client.GetAsync(new System.Uri("https://61769aed03178d00173dad89.mockapi.io/api/v1/transactions"));

            if (szResult.IsSuccessStatusCode)
            {
                var content = await szResult.Content.ReadAsStringAsync();
                Transaction_list = JsonConvert.DeserializeObject<List<cls_Transaction_Header>>(content);

            }

                var stock_adapter = new PackageAdapter(Activity, Transaction_list);
                list_view_Package = View.FindViewById<ListView>(Resource.Id.lstPackageHeader);

                RegisterForContextMenu(list_view_Package);

                list_view_Package.ChoiceMode = ChoiceMode.Single;

                list_view_Package.Adapter = stock_adapter;
                list_view_Package.ItemClick += OnListItemClick;
                Activity. RunOnUiThread(() => progressDialog.Hide());
           
        
        }
        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
           // cls_statics.NavigationView_Main_NavigationItemSelected(sender, e, this, drawerLayout);
        }
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
               // cls_statics.B_IS_PROMOTION = true;
                //bool str=bAddPromotion();
                Intent intent = new Intent(Activity, typeof(act_Transaction_Detail));
                var helperListViewObj = sender as ListView;
                var t = Transaction_list[e.Position];
                intent.PutExtra("PromoCode", Newtonsoft.Json.JsonConvert.SerializeObject(t.id));
                this.StartActivity(intent);
                //this.Dispose();
                Activity.Finish();
                
            }
            catch (Exception ex)
            {
                //T_ERROR_LOG err = new NGage.T_ERROR_LOG();
                //err.FileName = this.GetType().ToString();
                //err.ErrorMsg = ex.Message;
                //cls_database.InsertObjectData(err);
            }

        }
       
    }
}
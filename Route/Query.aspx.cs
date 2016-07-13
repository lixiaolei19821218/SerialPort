using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Query : System.Web.UI.Page
{
    private ScanCodeEntities repo = new ScanCodeEntities();

    protected void Page_Load(object sender, EventArgs e)
    {       
        string qrcode = Page.RouteData.Values["qrcode"].ToString();
        QRCode qrcodeEnt = repo.QRCodes.Find(qrcode);
        //获取同一订单中的所有二维码
        List<QRCode> qrcodeEnts = repo.QRCodes.Where(q => q.OrderNumber == qrcodeEnt.OrderNumber).ToList();
        int index = qrcodeEnts.IndexOf(qrcodeEnt);
        var barcodeEnts = repo.BarCodes.Where(b => b.OrderNumber == qrcodeEnt.OrderNumber);
        BarCode barcodeEnt = barcodeEnts.ElementAt(index);
        //通过barcodeEnt.Code查询品牌
        //qrcodeEnt.OrderNumber查询零售户
    }
}
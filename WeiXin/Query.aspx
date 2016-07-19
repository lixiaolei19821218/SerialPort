<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>首页</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <link rel="stylesheet" href="css/index.css" />
</head>
<body class="index" style="background: #EDF2F4">
    <header>
    </header>
    <div class="center">
        <ul>
            <li>
                <div class="listleft">销售区域</div>
                <div class="listright">四川省</div>
            </li>
            <li>
                <div class="listleft">零售户编码</div>
                <div class="listright" runat="server" id="custCode"></div>
            </li>
            <li>
                <div class="listleft">零售户姓名</div>
                <div class="listright" runat="server" id="custName"></div>
            </li>
            <li>
                <div class="listleft">经营地址</div>
                <div class="listright" runat="server" id="busiAddr"></div>
            </li>
            <li>
                <div class="listleft">销售日期</div>
                <div class="listright">2016-07-13</div>
            </li>
            <li>
                <div class="listleft">品牌</div>
                <div class="listright">云烟（软珍品）</div>
            </li>
            <li style="height: 4rem;">
                <div class="listleft">对应的32<br>
                    位激活码</div>
                <div class="listright">1234&nbsp;5678&nbsp;1234&nbsp;5678&nbsp;<br>
                    1234&nbsp;5678&nbsp;1234&nbsp;5678&nbsp;</div>
            </li>
            <li>
                <div class="listleft">扫描记录</div>
                <div class="listright">1次</div>
            </li>
        </ul>
    </div>
    <footer>
        <div class="footleft">
            <img src="img/bar2.png" /><span>点击关注官方微信</span>
        </div>
        <div class="footright">
            <img src="img/bar3.png" /><span>举报电话</span><p>12313</p>
        </div>
    </footer>
</body>
</html>

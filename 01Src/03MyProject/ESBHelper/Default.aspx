<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ESBHelper.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESBHelper</title>
    <script src="Assets/js/jquery-1.12.0.min.js"></script>
    <script src="js/mysoft.js"></script>
    <script src="js/Common.js"></script>
    <link href="Assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Assets/js/bootstrap.min.js"></script>
</head>
<body>

    <nav class="navbar navbar-default" role="navigation">
        <div class="navbar-header">
            <a class="navbar-brand" href="#">Workflow</a>
        </div>
        <div>
            <ul class="nav navbar-nav">
                <li class="active"><a href="#">ESB集成</a></li>
                <li><a href="#">其他1</a></li>
                <li><a href="#">其他2</a></li>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">其他3 
                       <b class="caret"></b>
                    </a>
                    <ul class="dropdown-menu">
                        <li><a href="#">开发中</a></li>
                        <li><a href="#">开发中</a></li>
                    </ul>
                </li>
                <li></li>
            </ul>
        </div>
        <div>
            <form class="navbar-form navbar-left" role="search">
                <div class="form-group">
                    <input type="text" class="form-control" placeholder="Search" style="width: 400px">
                </div>
                <button type="submit" class="btn btn-default">提交</button>
            </form>
        </div>
        <%-- 注册登录模块
            <div>
            <p class="navbar-text navbar-right">
                Signed in as 
                <a href="#" class="navbar-link">Shenjl</a>
            </p>
        </div>--%>
    </nav>
    <form id="form1" runat="server">
        <div id="myCarousel" class="carousel slide" style="text-align: center; margin: 0 auto; width: 680px; height: 300px">
            <!-- 轮播（Carousel）指标 -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
            </ol>
            <!-- 轮播（Carousel）项目 -->
            <div class="carousel-inner">
                <div class="item active">
                    <img src="images/1.jpg" alt="First slide" style="width: 680px; height: 300px">
                </div>
                <div class="item">
                    <img src="images/2.jpg" alt="Second slide" style="width: 680px; height: 300px">
                </div>
                <div class="item">
                    <img src="images/3.jpg" alt="Third slide" style="width: 680px; height: 300px">
                </div>
            </div>
            <!-- 轮播（Carousel）导航 -->
            <a class="carousel-control left" href="#myCarousel"
                data-slide="prev">&lsaquo;</a>
            <a class="carousel-control right" href="#myCarousel"
                data-slide="next">&rsaquo;</a>
        </div>
        <div>
            <table style="text-align: center; margin: 0 auto">
                <tr style="height: 34px">
                    <td>
                        <span class="label label-primary" style="float: left">快捷操作</span>
                    </td>
                    <td></td>
                </tr>
                <tr style="text-align: left">
                    <td>
                        <button type="button" id="btnFastTest" class="btn btn-primary">一键测试</button>
                        <button type="button" id="btnFastCreate" class="btn btn-primary">一键生成</button>
                        <%--<button type="button" id="btnFastDownload" class="btn btn-primary" runat="server">一键下载</button>--%>
                        <asp:Button ID="btnFastDownload1" runat="server" class="btn btn-primary" Text="一键下载" OnClick="DownLoadSQLFast" />
                        <button type="button" id="btnForZhuangBi" class="btn btn-primary" style="display:none">一键装逼</button>
                        <button type="button" id="btnExample" class="btn btn-primary">查看示例</button>
                        <button type="button" id="btnClear" class="btn btn-primary" style="display:none">一键清空</button>                        
                    </td>
                    <td></td>
                </tr>
                <%--                
                <tr style="height: 34px">
                    <td></td>
                    <td></td>
                </tr>--%>
                <tr style="height: 34px">
                    <td>
                        <span class="label label-primary" style="float: left">ESB站点数据库配置</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">服务器地址</span>
                            <input id="txtESBDataBaseServer" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库名称</span>
                            <input id="txtESBDataBaseName" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">端口</span>
                            <input id="txtESBDataBasePort" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库账号</span>
                            <input id="txtESBDataBaseUser" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库密码</span>
                            <input id="txtESBDataBasePassword" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="float: right">
                        <button type="button" id="btnTestEsbDBInfo" class="btn btn-primary">数据库连接测试</button>
                        <button type="button" id="btnCreateForESB" class="btn btn-primary">生成</button>
                        <button type="button" id="btnDownLoadForESB" class="btn btn-primary" style="display: none">下载</button>
                    </td>
                </tr>

                <tr style="height: 34px">
                    <td>
                        <span class="label label-primary" style="float: left">ERP站点(主)</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">服务器地址</span>
                            <input id="txtERPDataBaseServer" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">端口</span>
                            <input id="txtERPDataBasePort" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>


                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库账号</span>
                            <input id="txtERPDataBaseUserName" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库密码</span>
                            <input id="txtERPDataBasePassword" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库名称</span>
                            <input id="txtERPDataBaseNameForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">站点地址</span>
                            <input id="txtERPDomainForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统编码</span>
                            <input id="txtERPProviderNameForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统名称</span>
                            <input id="txtERPDisplayNameForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统标识</span>
                            <input id="txtERPSysSignForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group" id="divIsNewErpForTrunk">
                            <span class="input-group-addon" style="width: 110px">是否新工作流</span>
                            <%--<input id="txtERPIsNewErpForTrunk" type="text" class="form-control" placeholder="" style="width: 230px">--%>
                            <%-- <input type="radio" name="rdIsNewErpForTrunk" id="rdIsNewErpForTrunk1" value="1" checked>是
                            <input type="radio" name="rdIsNewErpForTrunk" id="rdIsNewErpForTrunk2" value="0">否--%>
                            <label class="checkbox-inline">
                                <input type="radio" name="rdIsNewErpForTrunk" id="rdIsNewErpForTrunk1" value="1" checked>是
                            </label>
                            <label class="checkbox-inline">
                                <input type="radio" name="rdIsNewErpForTrunk" id="rdIsNewErpForTrunk2" value="0">否                                
                            </label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="float: right">
                        <button type="button" id="btnTestERPDBInfoForTrunk" class="btn btn-primary">数据库连接测试</button>
                        <button type="button" id="btnCreateForTrunk" class="btn btn-primary">生成</button>
                        <button type="button" id="btnDownLoadForTrunk" class="btn btn-primary" style="display: none">下载</button>
                    </td>
                </tr>
                <tr style="height: 34px">
                    <td>
                        <span class="label label-primary" style="float: left">ERP站点(辅1)</span>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">服务器地址</span>
                            <input id="txtERPDataBaseServerForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">端口</span>
                            <input id="txtERPDataBasePortForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库账号</span>
                            <input id="txtERPDataBaseUserNameForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库密码</span>
                            <input id="txtERPDataBasePasswordForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">数据库名称</span>
                            <input id="txtERPDataBaseNameForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">站点地址</span>
                            <input id="txtERPDomainForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统编码</span>
                            <input id="txtERPProviderNameForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统名称</span>
                            <input id="txtERPDisplayNameForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="input-group">
                            <span class="input-group-addon" style="width: 110px">系统标识</span>
                            <input id="txtERPSysSignForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">
                        </div>
                    </td>
                    <td>
                        <div class="input-group" id="divIsNewErpForBranch1">
                            <span class="input-group-addon" style="width: 110px">是否新工作流</span>
                            <%--<input id="txtERPIsNewErpForBranch1" type="text" class="form-control" placeholder="" style="width: 230px">--%>
                             <label class="checkbox-inline">
                                <input type="radio" name="rdIsNewErpForBranch" id="rdIsNewErpForBranch1" value="1" checked>是
                            </label>
                            <label class="checkbox-inline">
                                <input type="radio" name="rdIsNewErpForBranch" id="rdIsNewErpForBranch2" value="0">否                                
                            </label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="float: right">
                        <button type="button" id="btnTestERPDBInfoForBranch1" class="btn btn-primary">数据库连接测试</button>
                        <button type="button" id="btnCreateForBranch1" class="btn btn-primary">生成</button>
                        <button type="button" id="btnDownLoadForBranch1" class="btn btn-primary" style="display: none">下载</button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

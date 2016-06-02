<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavaScript.aspx.cs" Inherits="Web.JavaScript" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>JavaScript测试页面</title>
     <script type="text/javascript">
        //var scope = "global";
        //function t() {
        //    console.log(scope);
        //    alert(scope);
        //    var scope = "local"
        //    console.log(scope);
        //    alert(scope);
        //}

        //var a = "Hello";
        //function test() {
        //    var a;
        //    alert(a);
        //    a = "World";
        //    alert(a);

        //var a = "Hello";
        //function test() {
        //    alert(a);
        //    //var a = "World";
        //    a = "World";
        //    alert(a);
        //}

        //var a = 1;
        //function test() {
        //    alert(a);
        //    var a = 2;
        //    alert(a);
        //}
        //test();
        //alert(a);

        var color = "red";
        function foo() {
            var color = 'blue';
            var p = function bar() { alert(this.color); }
            color = "black";

            return p;
        }

        //var getLocalVariable = foo();
        //getLocalVariable();// private variable 

        if (true) { var a = 11; }

        function foo() {
            var b = 22;
            c = 33;
        }

        $(document).ready(function () {
            //debugger;
            //foo();
            //console.log(a);
            //console.log(b);
            //console.log(c);

            //var getLocalVariable = foo();
            //getLocalVariable();// private variable 

            //var getLocalVariable = foo();
            //getLocalVariable();    //"private variable"

            //var a = 5;
            //a = new Object();

            //debugger;
            //var a = fun();
            //a(); //0
            //a(); //1
            //a = fun();
            //a(); //0
            //PeopleTest();
            //fun1();

            //PrototypeTest();

            //$("#p").click(function () {
            //    alert('haha');
            //});
        });

        function fun() {
            var i = 0;
            return function () {
                console.log(i++);
            }
        }

        function fun1() {
            for (var i = 0; i < 5; i++) { }
            alert(i);
        }

        function People(name) {
            this.name = name;
            //对象方法
            this.Introduce = function () {
                alert("My name is " + this.name);
            }
        }

        //类方法
        People.Run = function () {
            alert("I can run");
        }
        //原型方法
        People.prototype.IntroduceChinese = function () {
            alert("我的名字是" + this.name);
        }

        function PeopleTest() {

            //测试
            var p1 = new People("Windking");

            p1.Introduce();

            People.Run();

            p1.IntroduceChinese();
        }

        var fun = function () { };
        fun.prototype.hello = function () { alert("原型方法！"); }

        fun.hello = function () { alert("类方法！"); };

        function PrototypeTest() {
            var xx = new fun();
            xx.hello();
            fun.hello();
        }




         //   //关于JS数组的测试
         //   function myJsTest()
         //   {
         //       /*数组的创建*/
         //       var array = new Array();    //创建一个数组
         //       var array1 = new Array(3);  //创建一个数组，并且指定长度
         //       var array2 = new Array(1, 3, 4, 6, 7, 9, 10);   //创建一个数组，并进行赋初值
         //                                                       //alert(array.length+''+array1.length+array2.length); //037
         //                                                       //注：不要''的话，即为数字相加，结果为10

         //       /*数组元素的访问*/
         //       var element1 = array2[3];   //element1的值为6
         //                                   //alert(element1);
         //       array2[3] = 5;  //为array2[3]赋值 
         //                       //alert(element1);   //值为6
         //                       //alert(array2[3]);  //值为5

         //       /*数组元素的添加*/
         //       var length1 = array.push(1, 2, 3);
         //       //alert(array);  //值为1,2,3
         //       //alert(length1); //值为3
         //       var length2 = array2.unshift(0);
         //       //alert(array2);  //值为0,1,3,4,5,7,9,10
         //       var newArray2 = array2.splice(0, 1, 100, 101, 102);  //值为100,101,102,1,3,4,5,7,9,10
         //                                                            //splice(start,num[,element1,element2]),删从strat起的元素，删num个，用后面的element元素替换被删除的元素，返回被删除的元素
         //                                                            //alert(array2);
         //                                                            //alert(array2+'  '+newArray2);


         //       /*数组元素的删除*/
         //       var lastStr = array2.pop(); //移除最后一个元素并返回
         //                                   //alert(lastStr); //值为10
         //       var firstStr = array2.shift(); //移除第一个元素并返回
         //                                      //alert(firstStr); //值为100
         //                                      //alert(array2); //101,102,1,3,4,5,7,9
         //       var deleteStr = array2.splice(0, 2);
         //       //alert(array2); //1,3,4,5,7,9
         //       array2.splice(2, 3, 6, 7, 9, 10);
         //       //alert(array2); //1,3,6,7,9,10,9 还原啦哈哈哈,屁！！
         //       array2.splice(2, 5, 4, 6, 7, 9, 10);
         //       //alert(array2); //1,3,4,6,7,9,10 这才是真真的还原了


         //       /*数组元素的截取和合并*/
         //       var newArray3 = array2.slice(0, 2);
         //       //alert(newArray3); //值为1,3
         //       var newArray4 = array2.concat('我是一个小尾巴！');
         //       //alert(newArray4[7]); //值为'我是一个小尾巴！'

         //       /*数组的拷贝*/
         //       var newArray5 = array2.slice(0); //拷贝一个数组，返回拷贝的新数组
         //                                        //alert(newArray5);
         //       var newArray6 = array2.concat(); //拷贝一个数组，返回拷贝的新数组
         //                                        //alert(newArray6);

         //       /*数组元素的排序*/
         //       array2.reverse(); //值为10,9,7,6,4,3,1
         //                         //alert(array2);
         //       array2.sort();   //值为1,10,3,4,6,7,9 
         //                        //alert(array2);
         //                        //注：按字母顺序(即字符编码的顺序)进行排序，没有按大小进行排序
         //       var newArray7 = array2.sort(sortNumber); //排序函数
         //                                                //alert(array2); // arrayObject.sort(sortby)

         //       /*数组元素的字符串化*/
         //       var myStr = array2.join('|');
         //       //alert(myStr);  //值为 1|3|4|6|7|9|10  字符串

         //       /*字符串的数组化*/
         //       var myNewArray = myStr.split("|");
         //       alert(myNewArray);
         //   }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>
</body>
</html>

define(function (require, exports, module) {
    //引入依赖
    require('jquery');
    require('vue');
    require('autocomplete');




    function initParam() {
        //输入框焦点
        $('.searchIpt').focus();
    }

    function bindDom() {

        //点击按钮
        $('.searchBTN').click(function () {
            var key = $('.searchIpt').val();
            //ToDo:验证输入是否合法
            if(!key){return;}
            //记录搜索记录
            saveKey(key);
            if ($(this).attr('for') == "baidu") {
                window.open('http://www.baidu.com/s?wd=' + key);
            }
            else {
                window.open('https://www.google.com.hk/#newwindow=1&q=' + key);
            }
        });

        //回车事件
        $(document).keydown(function (event) {
            if (event.keyCode == 13) {
                $('.searchBTN').click();
            }
        });

        $('.uname,.topMenus').hover(function () {
            $('.topMenus').stop(true, true).fadeIn(200);
        }, function () {
            $('.topMenus').stop(true, true).delay(500).fadeOut(200);
        });

        $('.ctnerTab a').click(function () {
            if (!$(this).hasClass('on')) {
                $('.ctnerTab a').removeClass('on').eq($(this).index()).addClass('on');
                $('.rtNavs').stop(true, true).hide(200).eq($(this).index()).show(300);
            }
        });

        $('.menusWrapper a').click(function () {
            if (!$(this).hasClass('active')) {
                $('.menusWrapper a').removeClass('active').eq($(this).index()).addClass('active');
                $('.cbox').stop(true, true).animate({top: 318}, 100).hide().eq($(this).index()).animate({top: 0}, 400).show();
            }
        });

        $('.smallPics a').click(function () {
            if (!$(this).hasClass('active')) {
                $('.smallPics a').removeClass('active').eq($(this).index()).addClass('active');
                $('.picLink').stop(true, true).removeClass('active').eq($(this).index()).addClass('active');
            }
        });

        $('.titleT').hover(function () {
            if (!$(this).hasClass('on')) {
                $('.titleT').removeClass('on').eq($(this).index()).addClass('on');
                $('.topicB').stop(true, true).hide().eq($(this).index()).show();
            }
        });
    }

    function moduleInit(){

        //搜索框自动补全
        //$('.searchIpt').AutoComplete({
        //    'data': ['沈金龙', '邱蜀燕'],
        //    'itemHeight': 24,
        //    'width': 529
        //}).AutoComplete('show');

    }

    function saveKey(key){
        $.ajax({
            type: 'GET',
            url: '/Home/SearchData/save/key/' + key,
            dataType: 'json',
            success: function (data) {
                if (data) {
                    //TODO：成功提示
                    alert(data);
                    console.info(data);
                }
                else {
                    //TODO:提示：未找到对应的任务。
                    alert(data);
                    console.info(data);
                }
            },
            error:function(XMLHttpRequest, textStatus, errorThrown){
                console.info(XMLHttpRequest);
                console.info(textStatus);
                console.info(errorThrown);
            }
        });
    }

    function VueInit(){
        new Vue({
            el:'#btnSearchBD',
            data:{
                txtBaidu:'百度一下'
            }
        });
    }
    exports.init = function () {
        initParam();
        bindDom();
        moduleInit();
        VueInit();
    }

});
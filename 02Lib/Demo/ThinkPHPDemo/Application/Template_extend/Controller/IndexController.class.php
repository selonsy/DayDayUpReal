<?php
namespace Template_extend\Controller;
use Think\Controller;
class IndexController extends Controller { 
    public function index(){
        $this->assign('title','ThinkPHP示例：模板继承');
        $this->assign('var','这里是子模板定义内容!');
        $this->display();
    }

}
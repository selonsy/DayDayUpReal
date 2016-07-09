<?php
namespace Group\Controller;
use Think\Controller;
class IndexController extends Controller { 
    public function index(){
        $Model  =   D('Form');
        $this->assign('list',$Model->getList());
        $this->display();
    }

    public function read($id=0){
        $Model  =   D('Form');
        $this->assign('vo',$Model->getDetail($id));
        $this->display();
    }
}
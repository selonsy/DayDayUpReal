<?php
namespace Route\Controller;
use Think\Controller;
class BlogController extends Controller { 

    public function _initialize(){
		$this->vars =   !empty($_GET)?$_GET:'';
    }
    
    public function index() {
        $this->display();
    }
    
	public function category() {
		$this->display('index');
	}
    
	public function archive() {
		$this->display('index');
	}
    
	public function read(){
		$this->display('index');
	}
    
	public function view(){
		$this->display('index');
	}
}
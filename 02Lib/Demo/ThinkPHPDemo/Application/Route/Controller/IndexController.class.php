<?php
namespace Route\Controller;
use Think\Controller;
class IndexController extends Controller { 
    
    public function index() {
        redirect(__MODULE__.'/Blog/crud/');
    }
     
}
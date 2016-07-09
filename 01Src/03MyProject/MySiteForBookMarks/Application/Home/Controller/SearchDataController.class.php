<?php
namespace Home\Controller;

use Think\Controller;
use Selonsy\CommonAction;
class SearchDataController extends Controller
{
    public function index()
    {
        $this->display();
    }

    static function uuid2($prefix = "")
    {    //可以指定前缀
        $str = md5(uniqid(mt_rand(), true));
        $uuid = substr($str, 0, 32);
        return $prefix . $uuid;
    }

    public function save()
    {

//        $a = $_GET['A'];
//        $a1 = $_POST['A'];
//        var_dump($hehe);
//        var_dump($a1);

        $key = $_GET['key'];
        $sd =M('searchdata');
        $sdOld = $sd->where("'sdName'='邱蜀燕'")->find();



        //var_dump($sdOld->getField('sdName'));
        //var_dump($sdOld->getField('sdName')=='NULL');
        //var_dump($sdOld->getField('sdName')==NULL);
        var_dump(phpinfo());
        var_dump($sdOld==NULL);
        if($sdOld==NULL){
            //var_dump($sdOld->getField('sdName')==NULL);
            $data['sdGUID']= SearchDataController::uuid2();
            $data['sdName']= $key;
            $data['sdCount']=1;
            $data['sdCreateTime']= date('y-m-d h:i:s',time());
            $data['sdUserName']= '沈金龙'; //第一次搜索人
            $sd->add($data);
            echo "插入成功！";
        }
        else{
            $data['sdCount'] = $sdOld->getField('sdCount')+1;
            $data['sdModifyTime']=date('y-m-d h:i:s',time());
            $sd->where("sdName='$key'")->save($data);
            echo "更新成功！";
        }
    }
}
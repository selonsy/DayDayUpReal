<?php
namespace Rbac\Controller;
class IndexController extends CommonController {
    // 框架首页
    public function index() {
        C ( 'SHOW_RUN_TIME', false ); // 运行时间显示
        C ( 'SHOW_PAGE_TRACE', false );
        $this->display();
    }
}
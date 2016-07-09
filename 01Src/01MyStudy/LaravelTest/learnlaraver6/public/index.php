<?php
/**
 * Laravel - A PHP Framework For Web Artisans
 *
 * @package  Laravel
 * @author   Taylor Otwell <taylorotwell@gmail.com>
 */

/*
|--------------------------------------------------------------------------
| Register The Auto Loader
|--------------------------------------------------------------------------
|
| Composer provides a convenient, automatically generated class loader for
| our application. We just need to utilize it! We'll simply require it
| into the script here so that we don't have to worry about manual
| loading any of our classes later on. It feels nice to relax.
|
*/
//自动加载文件设置
require __DIR__.'/../bootstrap/autoload.php';

/*
|--------------------------------------------------------------------------
| Turn On The Lights
|--------------------------------------------------------------------------
|
| We need to illuminate PHP development, so let us turn on the lights.
| This bootstraps the framework and gets it ready for use, then it
| will load up this application so that we can run it and send
| the responses back to the browser and delight our users.
|
*/
//初始化服务容器（可以查看一下关于‘服务容器’的相关文档）
$app = require_once __DIR__.'/../bootstrap/app.php';

/*
|--------------------------------------------------------------------------
| Run The Application
|--------------------------------------------------------------------------
|
| Once we have the application, we can handle the incoming request
| through the kernel, and send the associated response back to
| the client's browser allowing them to enjoy the creative
| and wonderful application we have prepared for them.
|
*/

//通过服务容器生成一个kernel类的实例（Illuminate\Contracts\Http\Kernel实际上只是一个接口，真正生成的实例是App\Http\Kernel类，
//至于怎么把接口和类关联起来，请查看Contracts相关文档）
$kernel = $app->make('Illuminate\Contracts\Http\Kernel');

//运行Kernel类的handle方法，主要动作是运行middleware和启动URL相关的Contrller
$response = $kernel->handle(
	$request = Illuminate\Http\Request::capture()
);

//控制器返回结果之后的操作
$response->send();

$kernel->terminate($request, $response);

<?php 
function save_log($msg){
     global $ip,$dataLocal;
     $ipLog=date('y-m-d').".log";
     $log=fopen("$ipLog", "a+");
     fputs($log,$msg."\n");
     fclose($log);
}
$log = "[CheckList]";
$log .= "\nIP Public: ".$_SERVER['REMOTE_ADDR'];
$log .= "\nData: ". date('y-m-d h:m:i');
$log .= "\n+++++++++++++++++++++++++++++++++++++";
save_log($log);
echo base64_encode(file_get_contents("list.xml"));



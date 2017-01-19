
		
<html>
<head>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8">


<title>Admin Home page</title>

<link rel="stylesheet" type="text/css" href="../css/main.css" />

<script type="text/javascript" src="../js/jquery.js"></script>
<link rel="stylesheet" media="screen" type="text/css" href="../css/datepicker.css" />
<script type="text/javascript" src="../js/datePicker.js"></script>




<script type="text/javascript">
	$funciton({
		$('#date').DatePicker({
			flat: true,
			date: '2008-07-31',
			current: '2008-07-31',
			calendars: 1,
			starts: 1
		});
	});
</script>

</head>
<body>
	<center>
	
	<input type="text" id="date" name="name"  />
	</center>
</body>
</html>

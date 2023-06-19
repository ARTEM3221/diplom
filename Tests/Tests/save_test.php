<?php
    if (!isset($_POST['testXml']) || !isset($_POST['directory']) || !isset($_POST['testName'])) {
        http_response_code(400);
        echo "Не надані всі необхідні параметри";
        exit;
    }

    $testXml = $_POST['testXml'];
    $directory = $_POST['directory'];
    $testName = $_POST['testName'];

    $testsPath = __DIR__ . "/Tests/";

    if (!file_exists($testsPath . $directory)) {
        mkdir($testsPath . $directory, 0777, true);
    }

    $filePath = $testsPath . $directory . "/" . $testName . ".xml";

    if (file_put_contents($filePath, $testXml) === false) {
        http_response_code(500);
        echo "Помилка при збереженні файлу";
        exit;
    }

    echo "Тест успішно збережено";
?>
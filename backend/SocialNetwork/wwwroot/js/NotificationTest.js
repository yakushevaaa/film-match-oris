const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationhub")
    .withAutomaticReconnect([0, 2000, 5000, 10000, 20000])
    .build();

connection.on("ReceiveNotification", function (message) {
    alert(message);
});

async function startConnection() {
    try {
        await connection.start();
        console.log("Connected to SignalR");
    } catch (err) {
        console.error("Ошибка подключения к SignalR:", err);
        // Повторная попытка через 5 секунд
        setTimeout(startConnection, 5000);
    }
}

startConnection();
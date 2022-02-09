mergeInto(LibraryManager.library, {
    Login: function() {
        createUnityInstance(canvas, config, (progress) => {
            progressBarFull.style.width = 100 * progress + "%";
        }).then((unityInstance) => {
            Moralis.start({
                serverUrl: 'https://xersvsi2qsxo.usemoralis.com:2053/server',
                appId: 'nQ5dNn0kHgFhYnr92FveN95owhOMtEvrkbgMNLwW'
            });

            Moralis.authenticate().then((user) => {
                if (user) {
                    console.log(`User ${user.id} logged in`);
                } else {
                    console.log('User not logged in');
                }
            });

            unityInstance.SendMessage("WebPageObject", "LoginCallback", JSON.stringify(user));
        });
    }
});
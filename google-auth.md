# Login with Google steps

* Create a ```client_id``` from [Developer Console](https://console.cloud.google.com/apis/credentials)

* Load this .js script:
```html
<script src="https://apis.google.com/js/api.js"></script>
```

* Initialize Gapi

```JS
gapi.load("auth2", async function() {
    await gapi.auth2.init({ client_id: 'client_id, created in step 1' });
});
```

* SignIn
```js
const authInstance = gapi.auth2.getAuthInstance();
const result = authInstance.signIn();

// Handle the result.
console.log('Login result', result);
```

* Get ```access_token``` and send it to the backend to exchange for a local ```jwt```

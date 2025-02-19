const { test, expect, request } = require('@playwright/test');

const headers = {
  // 'Accept': 'application/json',
  // 'Content-Type': 'application/json',
  // 'ApiKey': 'DadJokes!'
}

test('should get one random joke', async ({  }) => {
    const apiContext = await request.newContext({ extraHTTPHeaders: headers, ignoreHTTPSErrors: true });
    const response = await apiContext.get("api/joke");  
    console.log(await response);
    const responseBody = await response.text();
    //     console.log(await response.json());
    console.log(await responseBody);
    expect(response.ok()).toBeTruthy();
    expect(response.status()).toBe(200);
    console.log('Found a joke!');
  });

test('should get a list of categories', async ({ }) => {
  const apiContext = await request.newContext({ extraHTTPHeaders: headers, ignoreHTTPSErrors: true });
  const response = await apiContext.get("/api/joke/Categories");
  const responseBody = await response.body();
  expect(responseBody.includes('Chicken'));
  console.log('Found a chicken category!');
});

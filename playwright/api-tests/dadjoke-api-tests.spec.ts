const { test, expect, request } = require('@playwright/test');

const headers = {
  'Accept': 'application/json',
  'Content-Type': 'application/json',
  'ApiKey': 'DadJokes!'
}

test('should get one random joke', async ({  }) => {
    const apiContext = await request.newContext({ extraHTTPHeaders: headers, ignoreHTTPSErrors: true });
    const response = await apiContext.get("api/joke");  
    console.log(await response.json());
    expect(response.ok()).toBeTruthy();
    expect(response.status()).toBe(200);
});

test('should get all jokes in one category', async ({ }) => {
  const apiContext = await request.newContext({ extraHTTPHeaders: headers, ignoreHTTPSErrors: true });
  const response = await apiContext.get("/api/joke/category/Chickens");
  const responseBody = await response.json()
  expect(responseBody[0]).toHaveProperty("category", "Chickens");
  console.log('Found a chicken joke!');
  console.log(responseBody[0]);
  expect(response.ok()).toBeTruthy();
  expect(response.status()).toBe(200);
});

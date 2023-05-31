const {env} = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:45581';

const PROXY_CONFIG = [
  {
    context: [
      "/api/Category/GetCategories",
      "/api/Category/GetCategory",
      "/api/Category/CreateCategory",
      "/api/Category/UpdateCategory",
      "/api/Category/DeleteCategory",
      "/api/Login",
      "/api/Logout",
      "/api/Register",
      "/api/TokenStillValid",
      "/api/Note/GetNotes",
      "/api/Note/GetNote",
      "/api/Note/CreateNote",
      "/api/Note/UpdateNote",
      "/api/Note/DeleteNote",
      "/api/User/GetInfo",
      "/api/User/UpdateUser",
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;

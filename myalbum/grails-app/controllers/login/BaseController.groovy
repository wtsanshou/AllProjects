package login

abstract class BaseController {
     def auth() {
	if(!session.user) {
	     redirect(uri:"/index.gsp")
	     return false
	}
     }
}
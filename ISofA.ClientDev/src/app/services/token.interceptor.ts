import {Injectable} from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

import { AuthService } from './auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authService.isLogedIn()) {
            const clonedRequest = req.clone({
                headers: req.headers
                            .set('Authorization', 'Bearer ' + this.authService.getToken())
            });

            return next.handle(clonedRequest);
        }

        return next.handle(req);
    }
}


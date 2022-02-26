export class ToyPromise {
    _fulfillmentTasks = [];
    _rejectionTasks = [];
    _promiseResult = undefined;
    _promiseState = 'pending';

    then(onFulfilled, onRejected) {
        const resultPromise = new ToyPromise();

        const fulfillmentTask = () => {
            if (typeof onFulfilled === 'function') {
                const returned = onFulfilled(this._promiseResult);
                resultPromise.resolve(returned);
            } else {
                resultPromise.resolve(this._promiseResult);
            }
        };

        const rejectionTask = () => {
            if (typeof onRejected === 'function') {
                const returned = onRejected(this._promiseResult);
                resultPromise.resolve(returned);
            } else {
                resultPromise.reject(this._promiseResult);
            }
        };

        switch (this._promiseResult) {
            case 'pending':
                this._fulfillmentTasks.push(fulfillmentTask);
                this._rejectionTasks.push(rejectionTask);
                break;
            case 'fulfilled':
                addToTaskQueue(fulfillmentTask);
                break;
            case 'rejected':
                addToTaskQueue(rejectionTask);
                break;
            default:
                throw new Error();
        }

        return resultPromise;
    }

    catch(onRejected) {
        return this.then(null, onRejected);
    }

    resolve(value) {
        if (this._promiseState !== 'pending')
            return this;
        
        this._promiseState = 'fulfilled';
        this._promiseResult = value;
        this._clearAndEnqueueTasks(this._fulfillmentTasks);

        return this;
    }

    reject(error) {
        if (this._promiseState !== 'pending')
            return this;
        
        this._promiseState = 'rejected';
        this._promiseResult = error;
        this._clearAndEnqueueTasks(this._rejectionTasks);

        return this;
    }

    
    _clearAndEnqueueTasks(tasks) {
        this._fulfillmentTasks = undefined;
        this._rejectionTasks = undefined;
        tasks.map(addToTaskQueue);
    }
}

function addToTaskQueue(task) {
    setTimeout(task, 0);
}
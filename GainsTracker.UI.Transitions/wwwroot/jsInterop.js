// Initialize the current position in the browser's history.
let currentIndex = (history.state && history.state.index) || 0;

// Ensure the initial history state includes a custom `index` for tracking.
if (!history.state || !('index' in history.state)) {
    history.replaceState(
        {index: currentIndex, state: history.state},
        document.title
    );
}

// Save references of the native `history.state` getter and methods.
const getState = Object.getOwnPropertyDescriptor(History.prototype, 'state').get;
const {pushState, replaceState} = history;

// Function to handle back/forward navigation events.
function onPopstate() {
    const state = getState.call(history);

    console.log("pop")
    // If `state` is undefined, set it to the next index (handles hash changes).
    if (!state) {
        replaceState.call(history, {index: currentIndex + 1}, document.title);
    }
    const index = state ? state.index : currentIndex + 1;

    // Determine navigation direction, dispatch an event (`forward` or `back`) for transitions.
    const direction = index > currentIndex ? 'forward' : 'back';
    window.dispatchEvent(new Event(direction));

    // Update the current index to the new position.
    currentIndex = index;
}

// Wrapper to modify `pushState` and `replaceState` methods for updating `index`.
function modifyStateFunction(func, n) {
    return (state, ...args) => {
        // Call the original method with the updated state.
        func.call(history, {index: currentIndex + n, state}, ...args);

        // Update the current index only if the call succeeds.
        currentIndex += n;
    };
}

// Modify the `state` getter to return only the real state without the `index`.
function modifyStateGetter(cls) {
    const {get} = Object.getOwnPropertyDescriptor(cls.prototype, 'state');

    // Override the getter to exclude the custom `index`.
    Object.defineProperty(cls.prototype, 'state', {
        configurable: true,
        enumerable: true,
        get() {
            return get.call(this).state; // Return only the actual state object.
        },
        set: undefined // Prevent modifications.
    });
}

// Apply the getter modification to `History` and `PopStateEvent`.
modifyStateGetter(History);
modifyStateGetter(PopStateEvent);

// Override `pushState` and `replaceState` to track navigation indexes.
history.pushState = modifyStateFunction(pushState, 1);
history.replaceState = modifyStateFunction(replaceState, 0);

// Attach the `onPopstate` handler to listen for navigation changes.
window.addEventListener('popstate', onPopstate);
window.addEventListener('hashchange', () => console.log('hashchange aaaaaaaaaaaaa'));

// JSInterop functionality for synchronizing navigation with Blazor.
let dotnetHelperPrimary = undefined;
let dotnetHelperSecondary = undefined;

// Function to initialize JSInterop for navigation.
export function init(dotnetHelper, isPrimary) {
    if (isPrimary) {
        dotnetHelperPrimary = dotnetHelper;

        let lastLocation = location.href;
        let isBackwards = false;

        // Function to invoke Blazor navigation methods
        let invokeTransition = () => {
            dotnetHelperPrimary.invokeMethodAsync('Navigate', isBackwards);
            dotnetHelperSecondary.invokeMethodAsync('Navigate', isBackwards);
        }

        // Periodically check for URL changes to detect navigation.
        // Although this approach is not ideal, a small setInterval like this should not produce any performance issues.
        setInterval(function () {
            if (lastLocation !== location.href) {
                lastLocation = location.href;
                invokeTransition();
                isBackwards = false; // Reset backward flag
            }
        }, 75);

        window.addEventListener('back', () => {
            isBackwards = true;
        });
    } else {
        dotnetHelperSecondary = dotnetHelper;
    }
}

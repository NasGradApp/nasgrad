import React from 'react';
import { connect } from 'react-redux';

const Home = props => (
    <div>
        <h1>NasGrad </h1>
        <hr />
        <p>NasGrad (OurCity) is platform for providing easy way to communicate with public services. The main point of interest is giving the users the possibility (and power) to suggest possible improvements and to notify local government what should be the priority in the future investments.</p>

        <p>The <code>nasgrad</code> repository is where we do development and there are several ways you can participate in the project, for example:</p>
        <ul>
            <li>Submit bugs and feature requests and help us verify as they are checked in.</li>
            <li>Review source code changes.</li>
        </ul>
        <h2>Contributing</h2>

        <h3>Create a branch</h3>
        <ul>
            <li><code>git checkout master</code> from any folder in your local <code>nasgrad</code> repository</li>
            <li><code>git pull origin master</code> to ensure you have the latest main code</li>
            <li><code>git checkout -b the-name-of-my-branch</code> (replacing the-name-of-my-branch with a suitable name) to create a branch</li>
        </ul>
        <h3>Make the change</h3>
        <ul>
            <li>Make sure to follow Coding Guidelines</li>
            <li>Save the files and check in the browser and mobile app</li>
            <li>Make sure that the new change doesn't already exist as functionality</li>
        </ul>
        <h3>Test the change</h3>
        <ul>
            <li>If possible, test any visual changes in all latest versions of common browsers, on both desktop and mobile.</li>
        </ul>
        <h3>Push it</h3>
        <ul>
            <li><code>git add - A && git commit -m "My message"</code> (replacing My message with a commit message, such as Fixed header logo on Android) to stage and commit your changes</li >
            <li><code>git push my-fork-name the-name-of-my-branch</code></li>
            <li>Go to the nasgrad repo and you should see recently pushed branches.</li >
            <li>Follow GitHub's instructions.</li>
        </ul>
        <h3>License</h3>
        <p>Licensed under the MIT License.</p>
    </div>
);

export default connect()(Home);

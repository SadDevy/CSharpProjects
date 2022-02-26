var anchors = document.querySelectorAll('[href*="://"]');
for (var anchor of anchors)
    if (!anchor.matches('[href^="http://internal.com"]'))
        anchor.style.color = 'orange';

//Не моё
let selector = 'a[href*="://"]:not([href^="http://internal.com"])';
let links = document.querySelectorAll(selector);

links.forEach(link => link.style.color = 'orange');
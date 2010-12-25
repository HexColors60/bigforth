#
# spec file for package bigforth
#
# Copyright (c) 2010 Bernd Paysan
#
# All modifications and additions to the file contributed by third parties
# remain the property of their copyright owners, unless otherwise agreed
# upon. The license for this file, and modifications and additions to the
# file, is the same license as for the pristine package itself (unless the
# license for the pristine package is not an Open Source License, in which
# case the license is the MIT License). An "Open Source License" is a
# license that conforms to the Open Source Definition (Version 1.9)
# published by the Open Source Initiative.

# Please submit bugfixes or comments via http://bugs.opensuse.org/
#

Release:      62.1
Group:        Development/Other
Version:      2.4.0
Name:         bigforth-%{version}
License:      GPL v3 or later
Group:        Development/Languages/Other
AutoReqProv:  on
Summary:      bigForth language
Url:          http://bigforth.sourceforge.net/
Source:       bigforth.tar.gz

%description
bigforth is a portable implementation of the ANS Forth language.
Its greatest advantage is the portable widget toolkit MINOS which
builds on top of it. There are a lot of similarites with GForth btw.

%prep
rm -rf $RPM_BUILD_ROOT

%setup -n %name

%build

%configure

make

%install

%makeinstall

%post

%postun

%clean
rm -rf $RPM_BUILD_ROOT

%files
%defattr(-,root,root)
%doc BUGS COPYING CREDITS README ToDo

%changelog
* Mon Sep 04 2006 Nicolas Lécureuil <neoclust@mandriva.org> 2.1.1-2mdv2007.0
- XDG

* Mon Nov 21 2005 Lenny Cartier <lenny@mandrakesoft.com> 2.1.1-1mdk
- 2.1.1

* Fri Jul 08 2005 Lenny Cartier <lenny@mandrakesoft.com> 2.1.0-1mdk
- 2.1.0

* Thu Jun 03 2004 Lenny Cartier <lenny@mandrakesoft.com> 2.0.11-1mdk
- 2.0.11

* Sat Mar 22 2003 Lenny Cartier <lenny@mandrakesoft.com> 2.0.9-1mdk
- 2.0.9

* Sat Feb 01 2003 Lenny Cartier <lenny@mandrakesoft.com> 2.0.4-3mdk
- rebuild

* Wed Jun 12 2002 Lenny Cartier <lenny@mandrakesoft.com> 2.0.4-2mdk
- fix unpacking
- use make rather than %%make


* Sat Jun 08 2002 Lenny Cartier <lenny@mandrakesoft.com> 2.0.4-1mdk
- 2.0.4